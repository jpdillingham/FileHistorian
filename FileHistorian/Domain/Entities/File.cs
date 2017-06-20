using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHistorian.Domain.Entities
{
    public class File
    {
        public string FullName { get; set; }
        public string Path
        {
            get
            {
                return System.IO.Path.GetFullPath(FullName);
            }
        }
        public string Name
        {
            get
            {
                return System.IO.Path.GetFileName(FullName);
            }
        }
        public long Size { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime AccessedOn { get; set; }
    }
}
