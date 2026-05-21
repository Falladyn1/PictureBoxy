using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;
using System.IO;

namespace PictureBoxy
{
    internal class Serialization
    {
        public void Save(object o)
        {
            Type t = o.GetType();
            MemberInfo[] members = t.GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

            using (StreamWriter writer = new StreamWriter("zapis.txt", true))
            {

                writer.WriteLine("[" + t.Name + "]");

                foreach (MemberInfo mem in members)
                {
                    object[] attributes = mem.GetCustomAttributes(true);

                    if (attributes.Length != 0 && mem.MemberType==MemberTypes.Field)
                    {
                        string key = mem.Name;
                        foreach (object attr in attributes)
                        {
                            DescriptionAttribute da = attr as DescriptionAttribute;
                            if (da != null)
                            {
                                key = da.Description;
                            }
                        }
                        object ob = ((FieldInfo)mem).GetValue(o);

                        if (ob != null)
                        {
                            writer.WriteLine(key + ": " + ob.ToString());
                        }
                        else
                        {
                            writer.WriteLine(key + ": null");
                        }
                        writer.WriteLine();
                    }
                }
                writer.WriteLine();
            }
        }

        public object Load(string FileName)
        {
            if (File.Exists(FileName))
                return null;
            

        }
        // funkcja Save(object o)
        // Type t = o.GetType()
        // MembersInfo[] members = t.GetMembers();

        //object Load(string fileName)
        //Type t = Type.GetType(nazwa klasy)
        //obj = Activator.CreateInstance(t)

    }
}
