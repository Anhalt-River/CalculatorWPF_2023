using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator
{
    public class ExtendedAnsClass
    {
        public double _Answer { get; set; }
        private int desc_id; 
        public int _Desc_id 
        {
            get
            {
                return desc_id;
            }
            set
            {
                switch (value)
                {
                    case 1:
                        _Desc = "Деление на ноль невозможно";
                        break;
                    case 2:
                        _Desc = "Неверный ввод";
                        break;
                    case 3:
                        _Desc = "Ноль попал в знаменатель!";
                        break;
                    default:
                        break;
                }

                desc_id = value;
            }
        }
        //1 = Деление на ноль
        //2 = Неверный ввод(log(0))
        public string _Desc { get; set; }

        public ExtendedAnsClass()
        {
            _Desc_id = -1;
            _Desc = null;
        }
    }
}
