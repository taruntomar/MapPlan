using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace DataModel
{
    [Table(Name ="MapTable")]
    public class Map
    {
        [Column(IsPrimaryKey=true)]
        public int ID { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string info { get; set; }

    }

     [Table(Name ="ServiceTable")]
    public class Service
    {
        [Column(IsPrimaryKey = true)]
        public int ID { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string info { get; set; }
    }
    [Table(Name ="CustomerTable")]
    public class Customer
    {
        [Column(IsPrimaryKey = true)]
        public int ID { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Info { get; set; }
    }
    [Table(Name ="LinkTable")]
    public class Link
    {
        [Column(IsPrimaryKey = true)]
        public int ID { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Info { get; set; }
    }
    [Table(Name = "NodeTable")]
    public class Node
    {
        [Column(IsPrimaryKey = true)]
        public int ID { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Info { get; set; }
    }

    [Table(Name = "ScriptTable")]
    public class Script
    {
       [Column(IsPrimaryKey=true)]
        public int ID { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Info { get; set; }
        [Column]
        public string Data { get; set; }
    }
}
