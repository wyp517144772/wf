﻿using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WF.Entity;

namespace WF.DAO
{
    public class WF_RuleDao
    {

        public bool save(WF_Rule entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                conn.Insert<WF_Rule>(entity);
                return true;
            }
        }
        public bool update(WF_Rule entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Update<WF_Rule>(entity);
            }
        }
        public bool del(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                WF_Rule Role = conn.Get<WF_Rule>(id);
                Role.IsDelete = 1;
                return conn.Update<WF_Rule>(Role);
            }
        }
        public int DelByTmpKey(string tmpKey)
        {
            string sql = @"    DELETE FROM WF_Rule WHERE Tmpkey=@tmpkey";

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Execute(sql, new { tmpkey = tmpKey });
            }
        }
        public List<WF_Rule> getAllByTmpKey(string tmpkey)
        {
            string sql = @"    SELECT
                             	wr.ID,
                             	wr.Tmpkey,
                             	wr.Rulekey,
                             	wr.BeginNodeKey,
                             	wr.EndNodekey,
                             	wr.Expression,
                             	wr.[Description],
                             	wr.CreateUserCode,
                             	wr.CreateTime,
                             	wr.UpdateUserCode,
                             	wr.UpdateTime,
                             	wr.[State],
                             	wr.IsDelete,
                             	wr.BeginX,
                             	wr.BeginY,
                             	wr.EndX,
                             	wr.EndY
                             FROM
                             	WF_Rule AS wr WHERE wr.Tmpkey=@tmpkey";
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Query<WF_Rule>(sql, new { tmpkey = tmpkey }).ToList();
            }
        }
        public List<WF_Rule> getRuleByTmpKeyAndBeginNodeKey(string tmpkey,string beginNodeKey)
        {
            string sql = @"      
                        SELECT wr.ID,
                               wr.Tmpkey,
                               wr.Rulekey,
                               wr.BeginNodeKey,
                               wr.EndNodekey,
                               wr.Expression,
                               wr.[Description],
                               wr.CreateUserCode,
                               wr.CreateTime,
                               wr.UpdateUserCode,
                               wr.UpdateTime,
                               wr.[State],
                               wr.IsDelete,
                               wr.BeginX,
                               wr.BeginY,
                               wr.EndX,
                               wr.EndY
                        FROM   WF_Rule AS wr
                        WHERE  wr.BeginNodeKey = @beginNodeKey
                               AND wr.Tmpkey = @tmpkey
                               AND wr.[State] = 1
                               AND wr.IsDelete = 0";
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Query<WF_Rule>(sql, new { tmpkey = tmpkey, beginNodeKey= beginNodeKey }).ToList();
            }
        }
    }
}


