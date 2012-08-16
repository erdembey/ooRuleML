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
    public class Assert : ICloneable, IComparable
    {
        public Assert()
        {
            formula = new ArrayList();
        }

        public Assert(Assert another)
        {
            Oid refOid = null;
            formula = new ArrayList();

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

            Assert other = new Assert((Assert)o);

            if (this.oid != null)
            {
                if (!this.Oid.Equals(other.Oid))
                {
                    return false;
                }
            }

            if (this.formula.Count != other.formula.Count)
            {
                return false;
            }

            for (int i = 0; i < formula.Count; i++)
            {
                if (!formula[i].Equals(other.Formula[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int code = 1;

            if (this.oid != null)
            {
                code *= this.oid.GetHashCode();
            }

            if (this.formula.Count > 0)
            {
                for (int i = 0; i < formula.Count; i++)
                {
                    code *= formula[i].GetHashCode();
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
            if (index < 2)
            {
                index++;
                return true;
            }
            return false;
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
            return new Assert(this);
        }

        public int CompareTo(object o)
        {
            Assert another = new Assert((Assert)o);

            if (another.formula != null && formula != null)
            {
                return ((AssertFormula)formula[0]).CompareTo((AssertFormula)another.formula[0]);
            }

            return 0;
        }

        private Oid oid;
        private ArrayList formula;

        [XmlIgnore]
        private int index = -1;
    }
}
