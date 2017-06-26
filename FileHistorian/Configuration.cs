using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHistorian
{
    public class DirectoryElement : ConfigurationElement
    {
        #region Public Properties

        [ConfigurationProperty("path", IsKey = true, IsRequired = true)]
        public string Path
        {
            get { return (string)this["path"]; }
        }

        #endregion Public Properties
    }

    public class DirectoryElementCollection : ConfigurationElementCollection
    {
        #region Protected Methods

        protected override ConfigurationElement CreateNewElement()
        {
            return new DirectoryElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DirectoryElement)element).Path;
        }

        #endregion Protected Methods
    }

    public class FileHistorianConfigurationSection : ConfigurationSection
    {
        #region Public Properties

        [ConfigurationProperty("Directories")]
        public DirectoryElementCollection Directories
        {
            get { return (DirectoryElementCollection)this["Directories"]; }
        }

        #endregion Public Properties
    }
}