using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FarPoint.Win.Spread;

namespace BizComponents
{
    public class CellNote
    {
        public static void SetNoteInformation(Cell cell, String note)
        {
            cell.BackColor = Color.Yellow;
            cell.Note = note;
            cell.NoteIndicatorColor = Color.Blue;
            cell.NoteStyle = NoteStyle.PopupNote;
            cell.NoteIndicatorSize = new Size(10, 10);
        }
    }
}
