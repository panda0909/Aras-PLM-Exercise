using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcsAutoCAD
{
    class sortArray : IComparable
    {
        private int key_num;
        private string label;
        private string value1;
        private string value2;
          public sortArray()
          {
              key_num = 0; label = ""; value1 = "";value2 = "";
          }
          public sortArray(int key_number, string str_label)
          {
              key_num = key_number;
              label = str_label;
         }
        public sortArray(int key_number, string str_label, string str_value1)
        {
            key_num = key_number;
            label = str_label;
            value1 = str_value1;
        }
        public sortArray(int key_number, string str_label,string str_value1,string str_value2)
        {
            key_num = key_number;
            label = str_label;
            value1 = str_value1;
            value2 = str_value2;
        }
        public void Show()
         {
             System.Diagnostics.Debug.WriteLine("({0}.{1}.{2}.{3})", key_num, label,value1,value2);
         }
        public int GetKeyNumber()
        {
            return key_num;
        }
         public string GetLabel()
         {
             return label;
         }
        public string GetValue1()
        {
            return value1;
        }
        public string GetValue2()
        {
            return value2;
        }
         int IComparable.CompareTo(object obj)
         {
             sortArray v = (sortArray)obj;
             return (v.key_num-key_num)*(-1);
         }
    }
}
