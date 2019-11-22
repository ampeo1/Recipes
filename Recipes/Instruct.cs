using System;
using System.Collections.Generic;
using System.Text;

namespace Recipes
{
    public class Instruct
    {
        public string Step { get; set; }
        private int _id;
        public Instruct(string str,int id)
        {
            _id = id;
            Step = str;
        }
        public void ChangeId()
        {
            _id--;
        }
        public int Id
        {
            get
            {
                return _id;
            }
        }
        public int NumberStep
        {
            get
            {
                return ++_id;
            }
        }
    }
}
