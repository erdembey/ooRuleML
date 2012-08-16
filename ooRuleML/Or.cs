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
    public class Or : ICloneable
    {
        public Or()
        {
            formula = new ArrayList();
        }

        public Or(Or another)
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
                    AndOrFormula[] items = (AndOrFormula[])another.Formula.Clone();
                    formula.Clear();
                    foreach (AndOrFormula item in items)
                    {
                        formula.Add(new AndOrFormula(item));
                    }
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }

            oid = refOid;
            isCorrected = another.isCorrected;
        }

        public override bool Equals(object o)
        {
            if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            Or other = new Or((Or)o);

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

        public int AddFormula(AndOrFormula item)
        {
            return formula.Add(item);
        }

        [XmlElement(ElementName = "formula")]
        public AndOrFormula[] Formula
        {
            get
            {
                AndOrFormula[] items = new AndOrFormula[formula.Count];
                formula.CopyTo(items);
                return items;
            }
            set
            {
                if (value == null) return;
                AndOrFormula[] items = (AndOrFormula[])value;
                formula.Clear();
                foreach (AndOrFormula item in items)
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
            return new Or(this);
        }

        [XmlIgnore]
        public bool isCorrect
        {
            get
            {
                return isCorrected;
            }
            set
            {
                isCorrected = value;
            }
        }

        private Oid oid;
        private ArrayList formula;

        [XmlIgnore]
        private int index = -1;
        [XmlIgnore]
        private bool isCorrected = false;
    }
}
