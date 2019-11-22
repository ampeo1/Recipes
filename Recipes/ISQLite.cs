using System;
using System.Collections.Generic;
using System.Text;

namespace Recipes
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }
}
