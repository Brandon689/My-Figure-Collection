using SQLite;
using System.Collections.Generic;

namespace SmartUI
{
    public class db
    {
        SQLiteConnection s = new(@"..\..\..\..\ami.db");
        public db()
        {
            s.CreateTable<MFCItem>();
        }
        public void i(MFCItem m)
        {
            s.Insert(m);
        }
        public IEnumerable<MFCItem> Load()
        {
            return s.Table<MFCItem>();
        }
    }
}
