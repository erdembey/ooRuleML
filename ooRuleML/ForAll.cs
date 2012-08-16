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
    public class ForAll : ICloneable
    {
        public ForAll()
        {
            declare = new ArrayList();
        }

        public ForAll(ForAll another)
        {
            Oid refOid = null;
            ForAllFormula refForallformula = null;
            declare = new ArrayList();

            try
            {
                if (another.Oid != null)
                {
                    refOid = (Oid)another.Oid.Clone();
                }

                if (another.Formula != null)
                {
                    refForallformula = (ForAllFormula)another.Formula.Clone();
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
            }
            catch (Exception e)
            {
                e.ToString();
            }

            oid = refOid;
            formula = refForallformula;
        }

        public override bool Equals(object o)
        {
            if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            ForAll other = new ForAll((ForAll)o);

            if (this.oid != null)
            {
                if (!this.Oid.Equals(other.Oid))
                {
                    return false;
                }
            }

            if (this.Formula != null)
            {
                if (!this.Formula.Equals(other.Formula))
                {
                    return false;
                }
            }

            if (Declare.Length != other.Declare.Length)
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

        public int AddDeclare(Declare item)
        {
            return declare.Add(item);
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
        public ForAllFormula Formula
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
            return new ForAll(this);
        }

        private Oid oid;
        private ArrayList declare;
        private ForAllFormula formula;

        [XmlIgnore]
        private int index = -1;
    }
}
