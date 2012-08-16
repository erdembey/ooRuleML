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
     * @author     M. Erdem ÇORAPÇIOÐLU
     * @copyright  (c) 2006-2012
     * @license    LGPL v3
     */
    public class Protect : ICloneable
    {
        public Protect()
        {
            formula = new ArrayList();
            warden = new ArrayList();
        }

        public Protect(Protect another)
        {
            formula = new ArrayList();
            warden = new ArrayList();
            Oid refOid = null;

            try
            {
                if (another.Oid != null)
                {
                    refOid = (Oid)another.Oid.Clone();
                }

                if (another.Formula != null)
                {
                    AssertFormula[] items = (AssertFormula[])another.Formula.Clone();
                    formula.Clear();
                    foreach (AssertFormula item in items)
                    {
                        formula.Add(new AssertFormula(item));
                    }
                }

                if (another.Warden != null)
                {
                    Warden[] items = (Warden[])another.Warden.Clone();
                    warden.Clear();
                    foreach (Warden item in items)
                    {
                        warden.Add(new Warden(item));
                    }
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }

            oid = refOid;
        }

        public override bool Equals(object o)
        {
            if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            Protect other = new Protect((Protect)o);

            if (this.oid != null)
            {
                if (!this.Oid.Equals(other.Oid))
                {
                    return false;
                }
            }

            if (Formula.Length != other.Formula.Length)
            {
                return false;
            }

            for (int i = 0; i < Formula.Length; i++)
            {
                if (!Formula[i].Equals(other.Formula[i]))
                {
                    return false;
                }
            }

            if (Warden.Length != other.Warden.Length)
            {
                return false;
            }

            for (int i = 0; i < Warden.Length; i++)
            {
                if (!Warden[i].Equals(other.Warden[i]))
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

            if (this.Formula.Length > 0)
            {
                for (int i = 0; i < Formula.Length; i++)
                {
                    code *= Formula[i].GetHashCode();
                }
            }

            if (this.Warden.Length > 0)
            {
                for (int i = 0; i < Warden.Length; i++)
                {
                    code *= Warden[i].GetHashCode();
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
                    return warden;
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

        public int AddWarden(Warden item)
        {
            return warden.Add(item);
        }

        [XmlElement(ElementName = "warden")]
        public Warden[] Warden
        {
            get
            {
                Warden[] items = new Warden[warden.Count];
                warden.CopyTo(items);
                return items;
            }
            set
            {
                if (value == null) return;
                Warden[] items = (Warden[])value;
                warden.Clear();
                foreach (Warden item in items)
                {
                    warden.Add(item);
                }
            }
        }

        public int AddFormula(AssertFormula item)
        {
            return formula.Add(item);
        }

        [XmlElement(ElementName = "formula")]
        public AssertFormula[] Formula
        {
            get
            {
                AssertFormula[] items = new AssertFormula[formula.Count];
                formula.CopyTo(items);
                return items;
            }
            set
            {
                if (value == null) return;
                AssertFormula[] items = (AssertFormula[])value;
                formula.Clear();
                foreach (AssertFormula item in items)
                {
                    formula.Add(item);
                }
            }
        }

        [XmlElement(ElementName = "oid")]
        public Oid Oid
        {
            get { return oid; }
            set { oid = value; }
        }

        public Object Clone()
        {
            return new Protect(this);
        }

        private Oid oid;
        private ArrayList formula;
        private ArrayList warden;

        [XmlIgnore]
        private int index = -1;
    }
}
