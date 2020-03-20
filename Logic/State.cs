using System;
using WhiteboardService.DataTypes;
using System.IO;
using System.Collections.Concurrent;
using GitEngineDB.Engine;
using System.Collections.Generic;
using System.Linq;

namespace WhiteboardService.Logic
{
    public class State
    {
        const int COMMIT = 1;
        private string _dbFolder;
        private string _userName;
        private string _userEmail;
        private ConcurrentDictionary<string, GitDbEngine> _db = new ConcurrentDictionary<string, GitDbEngine>();
                

        #region Static Constructor
        public State(string dbFolder, string userName, string userEmail)
        {
            _dbFolder = dbFolder;
            _userName = userName;
            _userEmail = userEmail;
            var directories = Directory.GetDirectories(_dbFolder);
            foreach(var dir in directories)
            {
                var boardId = Path.GetFileName(dir);
                _db.GetOrAdd(boardId, (i) => new GitDbEngine(dir, _userName, _userEmail));
            }
        }
        #endregion

        public string GetBoardData(string boardId)
        {            
            if(_db.TryGetValue(boardId, out var db))
            {
                return db.GetRawData(boardId);
            }
            return null;
        }
        
        public string CreateWhiteboard(string boardId = "")
        {         
            if (string.IsNullOrEmpty(boardId))
            {
                boardId = Guid.NewGuid().ToString("N");                
            }
            
            _db.GetOrAdd(boardId, (i) =>
            {
                var db = new GitDbEngine(Path.Combine(_dbFolder, boardId), _userName, _userEmail);
                db.SetData(boardId, new BoardUpdate { BoardId = boardId });
                return db;
            });
            
            return boardId;
            
        }

        internal List<string> GetBoardList()
        {
            return _db.Keys.ToList();
        }

        public void UpdateBoard(BoardUpdate update)
        {
            if (_db.TryGetValue(update.BoardId, out var db))
            {
                var stored = db.GetData<BoardUpdate>(update.BoardId);
                stored.Moves.AddRange(update.Moves);
                db.SetData(update.BoardId, stored);
                
                if(update.UpdateType == COMMIT)
                {
                    db.CommitChanges();
                }
            }
        }
    }
}