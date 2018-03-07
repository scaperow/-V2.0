using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Drawing;

namespace BizCommon
{
    public static class ResourceHelper
    {
        private static ResourceManager _resourceManager = null;
        private static ResourceManager ResourceManager
        {
            get
            {
                if (_resourceManager == null)
                    _resourceManager = new ResourceManager("Yqun.Client.Properties.IcoResource", typeof(ResourceHelper).Assembly);
                return _resourceManager;
            }

        }

        public static Bitmap GetImage(string name)
        {
            return ResourceManager.GetObject(name) as Bitmap;
        }
    }
}
