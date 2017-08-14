﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.Common;
using WF.Entity;
using Dapper;
using DapperExtensions;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WF.DAO
{
    public class WF_AgentDao
    {
        public List<WF_Agent> getAll(string origina, string user, int state, int begin, int end, out int count)
        {
            string sql = @"  ;WITH tmp 
                                        AS (
                                        	
                                        SELECT
                                        	wa.ID,
                                        	wa.AgentUserCode,
                                        	wa.AgentName,
                                        	wa.OriginalUserCode,
                                        	wa.OriginalUserName,
                                        	wa.BeginTime,
                                        	wa.EndTime,
                                        	wa.[State],
                                        	wa.CreateUserCode,
                                        	wa.CreateTime,
                                        	wa.UpdateUserCode,
                                        	wa.UpdateTime,
                                        	wa.IsDelete,
                                        	ROW_NUMBER () OVER (ORDER BY wa.CreateTime DESC  ) AS [index]
                                        FROM
                                        	WF_Agent AS wa
                                        WHERE (wa.OriginalUserCode LIKE '%'+@origina+'%' OR wa.OriginalUserName LIKE '%'+@origina+'%'   )
                                        AND (wa.AgentUserCode LIKE '%'+@user+'%' OR wa.AgentName LIKE '%'+@user+'%'   )
                                        AND (@state=-1 OR wa.[State]= @state)
                                        AND wa.IsDelete=0
                                        ) 
                                        SELECT * FROM tmp AS t WHERE t.[index] BETWEEN @begin AND @end";
            string sqlcount = @"   SELECT COUNT(1)
                                            FROM
                                            	WF_Agent AS wa
                                               WHERE (wa.OriginalUserCode LIKE '%'+@origina+'%' OR wa.OriginalUserName LIKE '%'+@origina+'%'   )
                                        AND (wa.AgentUserCode LIKE '%'+@user+'%' OR wa.AgentName LIKE '%'+@user+'%'   )
                                            AND (@state=-1 OR wa.[State]= @state)
                                            AND wa.IsDelete=0 ";
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                count = conn.Query<int>(sqlcount, new { origina = origina, user = user, state = state }).FirstOrDefault();
                return conn.Query<WF_Agent>(sql, new { origina = origina, user = user, state = state, begin = begin, end = end }).ToList();
            }
        }
    }
}


