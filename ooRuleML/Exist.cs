using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ooRuleML
{
    /*
     * ooRuleML C# Library
     *
     * @package    ooRuleML
     * @category   Library
     * @author     M. Erdem ÇORAPÇIOĞLU
     * @copyright  (c) 2006-2012
     * @license    LGPL v3
     */
    public class Exist : ICloneable
    {
        public Exist()
        {
            declare = new ArrayList();
        }

        public Exist(Exist another)
        {
            Oid refOid = null;
            QueryFormula refFormula = null;
            declare = new ArrayList();

            try
            {
                if (another.Oid != null)
                {
                    refOid = (Oid)another.Oid.Clone();
                }

                if (another.Declare != null)
                {
                    Declare[] items = (Declare[])another.Declare.Clone();
                    declare.Clear();
                    foreach (Declare item in items)
                    {
                        declare.Add(new Declare(item));
                    }
                }

                if (another.Formula != null)
                {
                    refFormula = (QueryFormula)another.Formula.Clone();
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }            

            oid = refOid;
            formula = refFormula;
        }

        public override bool Equals(object o)
        {
            if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            Exist other = new Exist((Exist)o);

            if (this.oid != null)
            {
                if (!this.Oid.Equals(other.Oid))
                {
                    return false;
                }
            }

            if (!this.Formula.Equals(other.Formula))
            {
                return false;
            }

            for (int i = 0; i < Declare.Length; i++)
            {
                if (!Declare[i].Equals(other.Declare[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int code = 1;

            if (this.Oid != null)
            {
                code *= this.Oid.GetHashCode();
            }

            if (this.Formula != null)
            {
                code *= this.Formula.GetHashCode();
            }

            if (this.Declare.Length > 0)
            {
                for (int i = 0; i < Declare.Length; i++)
                {
                    code *= Declare[i].GetHashCode();
                }
            }

            return code;
        }

        public void Reset()
        {
            index = -1;
        }

        [XmlIgnore]
        public object Current
        {
            get
            {
                if (index == 0)
                {
                    return oid;
                }
                else if (index == 1)
                {
                    return declare;
                }
                else if (index == 2)
                {
                    return formula;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool Next()
        {
            if (index < 3)
            {
                index++;
                return true;
            }
            return false;
        }

        [XmlElement(ElementName = "declare")]
        public Declare[] Declare
        {
            get
            {
                Declare[] items = new Declare[declare.Count];
                declare.CopyTo(items);
                return items;
            }
            set
            {
                if (value == null) return;
                Declare[] items = (Declare[])value;
                declare.Clear();
                foreach (Declare item in items)
                {
                    declare.Add(item);
                }
            }
        }

        [XmlElement(ElementName = "formula")]
        public QueryFormula Formula
        {
            get { return formula; }
            set { formula = value; }
        }

        [XmlElement(ElementName = "oid")]
        public Oid Oid
        {
            get { return oid; }
            set { oid = value; }
        }

        public Object Clone()
        {
            return new Exist(this);
        }

        private Oid oid;
        private ArrayList declare;
        private QueryFormula formula;

        [XmlIgnore]
        private int index = -1;
    }
}
