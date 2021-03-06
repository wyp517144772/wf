﻿using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF.Entity
{
    public class WF_Rule
    {
        public int ID { get; set; }
        public string Tmpkey { get; set; }
        public string Rulekey { get; set; }
        public string BeginNodeKey { get; set; }
        public string EndNodekey { get; set; }
        public string Expression { get; set; }
        public string Description { get; set; }
        public string CreateUserCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string UpdateUserCode { get; set; }
        public DateTime UpdateTime { get; set; }
        public int State { get; set; }
        public int IsDelete { get; set; }
        public float BeginX { get; set; }
        public float BeginY { get; set; }
        public float EndX { get; set; }
        public float EndY { get; set; }
    }

    [Serializable]
    public class WF_RuleMap : ClassMapper<WF_Rule>
    {
        public WF_RuleMap()
        {
            base.Table("WF_Rule");
            this.Map(f => f.ID).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)    
         
            AutoMap();
        }
    }
}


