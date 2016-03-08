using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace StatusMonitor.Helper
{
    public class FormHelper
    {
        public static void SetDoubleBuffered(System.Windows.Forms.Control ctrl, bool value)
        {
            try
            {

                if (ctrl == null) return;
                //DoubleBuffered   OptimizedDoubleBuffer
                PropertyInfo propinfo = ctrl.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                if (propinfo != null) propinfo.SetValue(ctrl, value, null);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("setDoubleBuffered:" + ex.Message);
            }
        }
    }
    public class StatusListViewItemComparer : System.Collections.IComparer
    {
        public int Column = 0;
        public bool Ascending = true;

        public StatusListViewItemComparer(int column, bool ascending)
        {
            Column = column;
            Ascending = ascending;
        }
        public int Compare(object x, object y)
        {
            // Get Value
            string tx = ((ListViewItem)x).SubItems[Column].Text;
            string ty = ((ListViewItem)y).SubItems[Column].Text;
            // Compare
            int ret = String.Compare(tx, ty);
            if (!Ascending) ret *= -1;
            if (ret == 0)
            {
                tx = ((ListViewItem)x).SubItems[0].Text;
                ty = ((ListViewItem)y).SubItems[0].Text;
                ret = String.Compare(tx, ty);
            }
            if (ret == 0)
            {
                tx = ((ListViewItem)x).SubItems[1].Text;
                ty = ((ListViewItem)y).SubItems[1].Text;
                ret = String.Compare(tx, ty);
            }
            return ret;
        }
    }

    public class TotalListViewItemComparer : System.Collections.IComparer
    {
        public int Column = 0;
        public bool Ascending = true;
        //private static bool[] ColumnItemInt = { false, true, true, true, true };
        private static bool[] ColumnItemInt = { false, true, true, true, true, false, true, true, true, true, true };

        public TotalListViewItemComparer(int column, bool ascending)
        {
            Column = column;
            Ascending = ascending;
        }
        public int Compare(object x, object y)
        {
            // Check Top
            int row = ((ListViewItem)x).Index;
            if (row == 0) return 0;
            // Get Value
            string tx = ((ListViewItem)x).SubItems[Column].Text;
            string ty = ((ListViewItem)y).SubItems[Column].Text;
            // Compare
            int ret = 0;
            if (ColumnItemInt[Column]) ret = Int32.Parse(tx).CompareTo(Int32.Parse(ty));
            else ret = String.Compare(tx, ty);

            //update,xzg,2014/05/13,S
            //return (Ascending ? ret: -ret);
            if (!Ascending) ret *= -1;
            //update,xzg,2014/05/13,E

            //add,2014/05/13
            if (ret == 0)
            {
                tx = ((ListViewItem)x).SubItems[0].Text;
                ty = ((ListViewItem)y).SubItems[0].Text;
                ret = String.Compare(tx, ty);
            }
            return ret;
        }
    }
}
