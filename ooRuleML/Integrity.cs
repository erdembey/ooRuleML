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
    public class Integrity : ICloneable
    {
        public Integrity()
        {
        }

        public Integrity(Integrity another)
        {
            Oid refOid = null;
            Atom refAtom = null;
            Or refOr = null;
            And refAnd = null;
            AndOrFormula refFormula = null;

            try
            {
                if (another.Oid != null)
                {
                    refOid = (Oid)another.Oid.Clone();
                }

                if (another.Atom != null)
                {
                    refAtom = (Atom)another.Atom.Clone();
                }

                if (another.InnerOr != null)
                {
                    refOr = (Or)another.InnerOr.Clone();
                }

                if (another.InnerAnd != null)
                {
                    refAnd = (And)another.InnerAnd.Clone();
                }

                if (another.Formula != null)
                {
                    refFormula = (AndOrFormula)another.Formula.Clone();
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }

            oid = refOid;
            and = refAnd;
            or = refOr;
            formula = refFormula;
            atom = refAtom;
        }

        /*        
         * private Atom atom;
         * private Or or;
         * private And and;
         * private Oid oid;
         * private AndOrFormula formula;
         */

        public override bool Equals(object o)
        {
            if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            Integrity other = new Integrity((Integrity)o);

            if (this.Atom != null)
            {
                if (!this.Atom.Equals(other.Atom))
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

            if (this.InnerOr != null)
            {
                if (!this.InnerOr.Equals(other.InnerOr))
                {
                    return false;
                }
            }

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

            return true;
        }

        public override int GetHashCode()
        {
            int code = 1;

            if (this.Atom != null)
            {
                code *= this.Atom.GetHashCode();
            }

            if (this.InnerAnd != null)
            {
                code *= this.InnerAnd.GetHashCode();
            }

            if (this.InnerOr != null)
            {
                code *= this.InnerOr.GetHashCode();
            }

            if (this.Oid != null)
            {
                code *= this.Oid.GetHashCode();
            }

            if (this.Formula != null)
            {
                code *= this.Formula.GetHashCode();
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
                    return and;
                }
                else if (index == 2)
                {
                    return or;
                }
                else if (index == 3)
                {
                    return atom;
                }
                else if (index == 4)
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
            if (index < 5)
            {
                index++;
                return true;
            }
            return false;
        }


        [XmlElement(ElementName = "oid")]
        public Oid Oid
        {
            get { return oid; }
            set { oid = value; }
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

        [XmlElement(ElementName = "formula")]
        public AndOrFormula Formula
        {
            get { return formula; }
            set { formula = value; }
        }

        public Object Clone()
        {
            return new Integrity(this);
        }

        private Atom atom;
        private Or or;
        private And and;
        private Oid oid;
        private AndOrFormula formula;

        [XmlIgnore]
        private int index = -1;
    }
}
