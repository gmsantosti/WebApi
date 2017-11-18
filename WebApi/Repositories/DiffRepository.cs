using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class DiffRepository
    {

        List<Diff> _diffs = new List<Diff>();
        static DiffRepository _instance;

        private DiffRepository() { }

        public static DiffRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DiffRepository();
            }
            return _instance;
        }

        public void SaveLeft(string id, string left)
        {
            var diff = _diffs.FirstOrDefault(d => d.Id == id);

            if (diff != null)
            {
                diff.Left = left;
            }
            else
            {
                _diffs.Add(new Diff { Id = id, Left = left });
            }

        }

        public void SaveRight(string id, string right)
        {
            var diff = _diffs.FirstOrDefault(d => d.Id == id);

            if (diff != null)
            {
                diff.Right = right;
            }
            else
            {
                _diffs.Add(new Diff { Id = id, Right = right });
            }
        }

        public Diff Get(string id)
        {
            var diff = _diffs.FirstOrDefault(d => d.Id == id);
            return diff;
        }
    }
}