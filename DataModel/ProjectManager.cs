﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace DataModel
{
    public class Project:DataContext
    {
        public Table<Script> Scripts;
        public Table<Map> Maps;
        public Table<Node> Nodes;
        public Table<Link> Links;
        public Table<Customer> Customers;
        public Project(string connection) : base(connection) { }

    }
    public class ProjectManager 
    {
        private string m_Templocation = null;
        private string m_ProjectName = null;
        private string m_projectPath = null;
        private string m_databasefilepath = null;
        private string m_projectInfoFileAddress = null;
        public Project CurrentProject { get; set; }
        public ProjectInfo CurrentProjectInfo { get; set; }

 

        public bool OpenProject(string fileaddress)
        {
            try{
                FileStream fw = new FileStream(fileaddress,FileMode.Open,FileAccess.ReadWrite,FileShare.Read);
                
                XmlSerializer xmlserializer = new XmlSerializer(typeof(ProjectInfo));
                ProjectInfo projectinfo = (ProjectInfo)xmlserializer.Deserialize(fw);
                m_projectInfoFileAddress = fileaddress;
                CurrentProject = new Project(projectinfo.DatabaseFilePath);
                if (!CurrentProject.DatabaseExists())
                    new Exception();
                
            }
            catch(Exception e){
                return false;
            }
            return true;
        }

        public void SaveProject()
        {
        }

        public void SaveProject(string p)
        {
        }

        public void NewProject(string projectname, string projectlocation)
        {
            m_projectPath = projectlocation  + projectname;
            m_ProjectName = projectname;
            if (Directory.Exists(m_projectPath))
                return;
            Directory.CreateDirectory(m_projectPath);
            // create maps directory
            Directory.CreateDirectory(m_projectPath + "\\Maps");

            // create database file
            m_databasefilepath = m_projectPath + "\\MapData.mdf";
            
            //string connectionstring = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Projects\AccessControl\WebSite\App_Data\permissions.mdf;Integrated Security=True;User Instance=True";
            CurrentProject = new Project("c:\\myfile.mdf");
            //CurrentProject.Connection.ConnectionString = "server=localhost\\;integrated security=SSPI;user instance=true;multipleactiveresultsets=true";
            CurrentProject.CreateDatabase();

            // create project Info file
            CurrentProjectInfo = new ProjectInfo();
            CurrentProjectInfo.ProjectFileVersion = "1.0";
            CurrentProjectInfo.ProjectName = projectname;
            CurrentProjectInfo.DatabaseFilePath = "/MapData.mdf";
            CurrentProjectInfo.DateCreated = DateTime.Now;
            CurrentProjectInfo.DateModified = DateTime.Now;
            CurrentProjectInfo.MapsPath = "/Maps/";
            CurrentProjectInfo.Owner = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            //CurrentProjectInfo.ScriptsPath = "/Scripts";
            m_projectInfoFileAddress = m_projectPath + "\\" + m_ProjectName + ".mpn";
            XmlSerializer xmlserializer = new XmlSerializer(typeof(ProjectInfo));
            StreamWriter sw = new StreamWriter(m_projectInfoFileAddress);
            xmlserializer.Serialize(sw, CurrentProjectInfo);
            sw.Close();
            
        }

        public void SaveScript(string ScriptName, string scriptdata)
        {
            string scriptpath = m_projectPath+"\\Scripts";
            string filepath = scriptpath + "\\" + ScriptName + ".mps";
            if (!Directory.Exists(scriptpath))
                Directory.CreateDirectory(scriptpath);

            using (StreamWriter sw = File.CreateText(filepath))
            {
                sw.Write(scriptdata);
            }
        }
    }

}
