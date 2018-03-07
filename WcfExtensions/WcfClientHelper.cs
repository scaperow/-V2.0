/**
 * File name: WcfClientHelper.cs 
 * Author: Mosfiqur.Rahman
 * Date: 11/1/2009 4:14:51 PM format: MM/dd/yyyy
 * 
 * 
 * Modification history:
 * Name				Date					Desc
 * 
 *  
 * Version: 1.0
 * */

#region Using Directives

#endregion

namespace WcfExtensions
{
    /// <summary>
    /// Summary of this class.
    /// </summary>
    public class WcfClientHelper
    {
        #region Member Variables

        #endregion

        #region Constructors

        #endregion

        #region Properties

        #endregion

        #region Methods

        public static T GetProxy<T>(string externalConfigPath)
        {
            var channelFactory = new ExtendedChannelFactory<T>(externalConfigPath);
            channelFactory.Open();
            return channelFactory.CreateChannel();
        }

        #endregion
    }
}

// end of namespace
