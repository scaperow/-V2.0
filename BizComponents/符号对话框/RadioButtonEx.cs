using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace BizComponents
{
    public class RadioButtonEx : RadioButton
    {
        public RadioButtonEx()
        {
            SetStyle(ControlStyles.Selectable, false);

            base.Appearance = Appearance.Button;
            base.AutoCheck = false;
            base.AutoSize = false;
        }

        [DefaultValue(typeof(System.Windows.Forms.Appearance),"Button")]
        public new Appearance Appearance
        {
            get
            {
                return base.Appearance;
            }
            set
            {
                base.Appearance = Appearance.Button;
            }
        }

        [DefaultValue(false)]
        public new Boolean AutoCheck
        {
            get
            {
                return base.AutoCheck;
            }
            set
            {
                base.AutoCheck = false;
            }
        }
    }
}
