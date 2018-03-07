using System;
using System.Collections.Generic;
using System.Text;

namespace BizComponents
{
    public interface IPage
    {
        void MoveFirst();
        void MoveLast();
        void MovePrev();
        void MoveNext();

        int Current
        {
            get;
        }

        int PageCount
        {
            get;
        }

        int ItemCount
        {
            get;
            set;
        }
    }
}
