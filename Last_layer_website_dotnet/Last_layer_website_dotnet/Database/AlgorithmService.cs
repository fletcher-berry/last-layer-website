using Last_layer_website_dotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Last_layer_website_dotnet.Database
{
    public class AlgorithmService : IAlgorithmService
    {
        public string ConnectionString;

        // mapping from case types to their identification strings in the database
        private static readonly Dictionary<CaseType, string> CaseTypeMap = new Dictionary<CaseType, string>()
        { { CaseType.OLL, "OLL"}, {CaseType.OLLCP, "OLLCP" }, {CaseType.OneLookLL, "1LLL"}, {CaseType.PLL, "PLL" } };

        public AlgorithmService(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public AlgPage GetOll()
        {
            var conn = new MySqlConnection(ConnectionString);
            conn.Open();
            var query = @"SELECT algorithm.alg, algorithm.source_id, alg_case.case_number, algorithm.modified
FROM algorithm JOIN alg_case ON algorithm.case_id = alg_case.id
WHERE alg_case.type = 'OLL'
ORDER BY alg_case.case_number";
            var cmd = new MySqlCommand(query, conn);
            var reader = cmd.ExecuteReader();
            var cases = new List<AlgCase>();
            var sourceMap = new Dictionary<int, int>(); // maps sourceId to incrementing source numbers
            while(reader.Read())
            {
                var algorithm = reader[0].ToString();
                int? sourceId = reader[1].Equals(DBNull.Value) ? null : (int?)Convert.ToInt32(reader[1]);
                var caseNumber = Convert.ToInt32(reader[2]);
                var modified = Convert.ToBoolean(reader[3]);
                if(cases.Count == 0 || caseNumber != cases.Last().CaseNumber)   // new case
                {
                    cases.Add(new AlgCase(CaseType.OLL, caseNumber));
                }
                int? sourceNumber = null;
                if (sourceId != null)
                {
                    if (sourceMap.ContainsKey((int)sourceId))
                        sourceNumber = sourceMap[(int)sourceId];
                    else
                    {
                        sourceMap.Add((int)sourceId, sourceMap.Count);
                        sourceId = sourceMap.Count;
                    }
                }
                cases.Last().Algorithms.Add(new Algorithm(algorithm, sourceNumber, modified));
            }
            reader.Close();
            var idMap = GetSources(sourceMap.Keys.ToList(), conn);
            conn.Close();
            var sourceArray = new Source[sourceMap.Count];
            var enumerator = sourceMap.GetEnumerator();
            foreach (var entry in sourceMap)
            {
                sourceArray[entry.Value] = idMap[entry.Key];
            }

            return new AlgPage(cases, sourceArray.ToList(), CaseType.OLL);
        }

        public AlgPage GetLastLayer(CaseType caseType, int superCaseNumber)
        {
            var conn = new MySqlConnection(ConnectionString);
            conn.Open();
            var query = @"SELECT algorithm.alg, algorithm.source_id, case_a.case_number, algorithm.modified
FROM algorithm JOIN alg_case case_a ON algorithm.case_id = case_a.id
JOIN alg_case case_b ON case_a.super_case_id = case_b.id
WHERE case_a.type = @caseType AND case_b.case_number = @superCaseNumber
ORDER BY case_a.case_number";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@caseType", CaseTypeMap[caseType]);
            cmd.Parameters.AddWithValue("@superCaseNumber", superCaseNumber);
            var reader = cmd.ExecuteReader();
            var cases = new List<AlgCase>();
            var sourceMap = new Dictionary<int, int>(); // maps sourceId to incrementing source numbers
            while (reader.Read())
            {
                var algorithm = reader[0].ToString();
                int? sourceId = reader[1].Equals(DBNull.Value) ? null : (int?)Convert.ToInt32(reader[1]);
                var caseNumber = Convert.ToInt32(reader[2]);
                var modified = Convert.ToBoolean(reader[3]);
                if (cases.Count == 0 || caseNumber != cases.Last().CaseNumber)   // new case
                {
                    cases.Add(new AlgCase(caseType, caseNumber));
                }
                int? sourceNumber = null;
                if (sourceId != null)
                {
                    if (sourceMap.ContainsKey((int)sourceId))
                        sourceNumber = sourceMap[(int)sourceId];
                    else
                    {
                        sourceMap.Add((int)sourceId, sourceMap.Count);
                        sourceId = sourceMap.Count;
                    }
                }
                cases.Last().Algorithms.Add(new Algorithm(algorithm, sourceNumber, modified));
            }
            reader.Close();
            var idMap = GetSources(sourceMap.Keys.ToList(), conn);
            conn.Close();
            var sourceArray = new Source[sourceMap.Count];
            var enumerator = sourceMap.GetEnumerator();
            foreach (var entry in sourceMap)
            {
                sourceArray[entry.Value] = idMap[entry.Key];
            }

            return new AlgPage(cases, sourceArray.ToList(), caseType, superCaseNumber);
        }

        /// <summary>
        /// Given a list of source ids, return mapping from id to Source object
        /// </summary>
        /// <param name="sourceIds"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public Dictionary<int, Source> GetSources(List<int> sourceIds, MySqlConnection conn)
        {
            sourceIds.Sort();
            var query = @"SELECT source.id, source.description, general_source.description, source.url 
FROM source JOIN general_source ON source.general_source_id = general_source.id
ORDER BY source.id"; 
            var cmd = new MySqlCommand(query, conn);
            var reader = cmd.ExecuteReader();
            var sources = new Dictionary<int, Source>();        
            int currSourceIdx = 0;
            while(reader.Read())
            {
                int sourceId = Convert.ToInt32(reader[0]);
                if(sourceId == sourceIds[currSourceIdx])
                {
                    var sourceDesc = reader[1].ToString();
                    var generalSourceDesc = reader[2].ToString();
                    var url = reader[3].ToString();
                    var source = new Source(sourceDesc, generalSourceDesc, url);
                    sources.Add(sourceId, source);
                    currSourceIdx++;
                    if (currSourceIdx >= sourceIds.Count)
                        break;
                }
            }
            return sources;
        }
    }
}
