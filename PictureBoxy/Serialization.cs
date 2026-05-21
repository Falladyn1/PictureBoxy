using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;

namespace PictureBoxy
{
    internal class Serialization
    {
        public void Save(object o)
        {
            Type t = o.GetType();
            MemberInfo[] members = t.GetMembers();

            foreach (MemberInfo mem in members)
            {
                object[] attributes = mem.GetCustomAttributes(true);

                if (attributes.Length != 0 && mem.MemberType==MemberTypes.Field)
                {
                    string key = "";
                    foreach (object attr in attributes)
                    {
                        DescriptionAttribute da = attr as DescriptionAttribute;
                        if (da != null)
                        {
                            key = da.Description;
                        }
                    }
                    object ob = mem.ReflectedType.GetField(mem.Name).GetValue(this);

                    if (ob != null)
                    {
                        dicField.Add(key, mem.ReflectedType.GetField(mem.Name).GetValue(this).ToString();
                    }
                    else
                    {

                    }
                }
            }
        }

        // funkcja Save(object o)
        // Type t = o.GetType()
        // MembersInfo[] members = t.GetMembers();

    }
}
