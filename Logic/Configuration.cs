using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using WhiteboardService.Logging;

namespace WhiteboardService.Logic
{
    public class WBConfig
    {
        private const string DEFAULT_REPO_PATH = "BoardStorage";
        private const string DEFAULT_GIT_EMAIL = "server@whiteboard.com";
        private const string DEFAULT_GIT_USER = "Whiteboard Server";
        private Lazy<int> _commitInterval;

        public WBConfig(IConfiguration config)
        {
            Raw = config;
            _commitInterval = new Lazy<int>(() => {
                if(!int.TryParse(Raw["Whiteboard:AutoCommitInterval"], out int interval))
                {
                    interval = 5000;
                }
                return interval;
            });
        }

        public IConfiguration Raw {get; private set;}
        public string GitUserEmail
        {
            get
            {
                var email = Raw["Whiteboard:GitUserEmail"];
                if(string.IsNullOrEmpty(email))
                {
                    return DEFAULT_GIT_EMAIL;
                }
                return email;
            }
        }
        public string GitUserName
        {
            get
            {
                var name = Raw["Whiteboard:GitUserName"];
                if(string.IsNullOrEmpty(name))
                {
                    return DEFAULT_GIT_USER;
                }
                return name;
            }
        }
        public string RepoFolder 
        { 
            get
            {
                var repoPath = Raw["Whiteboard:RepoFolder"];
                if(string.IsNullOrEmpty(repoPath))
                {
                    return DEFAULT_REPO_PATH;
                }
                return repoPath;
            } 
        }        
        public string[] BoardIds
        {
            get
            {
                return Directory.GetDirectories(RepoFolder)
                                .Select(d => Path.GetFileName(d))
                                .ToArray();
            }
        }
        public int AutoCommitInterval
        { 
            get
            {
                return _commitInterval.Value;
            } 
        }
    }
}