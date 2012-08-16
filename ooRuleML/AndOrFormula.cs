using System;
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
    public class AndOrFormula : ICloneable
    {
        public AndOrFormula()
        {
        }

        public AndOrFormula(AndOrFormula another)
        {
            Atom refAtom = null;
            And refAnd = null;
            Or refOr = null;

            try
            {
                if (another.Atom != null)
                {
                    refAtom = (Atom)another.Atom.Clone();
                }

                if (another.InnerAnd != null)
                {
                    refAnd = (And)another.InnerAnd.Clone();
                }

                if (another.InnerOr != null)
                {
                    refOr = (Or)another.InnerOr.Clone();
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }

            atom = refAtom;
            and = refAnd;
            or = refOr;
        }

        public override bool Equals(object o)
        {
            if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            AndOrFormula other = new AndOrFormula((AndOrFormula)o);

            if (this.Atom != null)
            {
                if (!this.Atom.Equals(other.Atom))
                {
                    return false;
                }
            }

            if (this.InnerOr != null)
            {
                if (!this.InnerOr.Equals(other.InnerOr))
                {
                    return false;
                }
            }

            if (this.InnerAnd != null)
            {
                if (!this.InnerAnd.Equals(other.InnerAnd))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int code = 1;

            if (this.InnerAnd != null)
            {
                code *= this.InnerAnd.GetHashCode();
            }

            if (this.Atom != null)
            {
                code *= this.Atom.GetHashCode();
            }

            if (this.InnerOr != null)
            {
                code *= this.InnerOr.GetHashCode();
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
                    return atom;
                }
                else if (index == 1)
                {
                    return and;
                }
                else if (index == 2)
                {
                    return or;
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

        [XmlElement(ElementName = "Atom")]
        public Atom Atom
        {
            get { return atom; }
            set { atom = value; }
        }

        [XmlElement(ElementName = "Or")]
        public Or InnerOr
        {
            get { return or; }
            set { or = value; }
        }

        [XmlElement(ElementName = "And")]
        public And InnerAnd
        {
            get { return and; }
            set { and = value; }
        }

        public Object Clone()
        {
            return new AndOrFormula(this);
        }

        private Atom atom;
        private Or or;
        private And and;

        [XmlIgnore]
        private int index = -1;
    }
}
