//
// This file was generated by the JavaTM Architecture for XML Binding(JAXB) Reference Implementation, v2.2.4-2 
// See <a href="http://java.sun.com/xml/jaxb">http://java.sun.com/xml/jaxb</a> 
// Any modifications to this file will be lost upon recompilation of the source schema. 
// Generated on: 2015.08.06 at 12:26:07 AM BRT 
//


package hpcshelf.certification.hash.component;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlAttribute;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for InterfaceRefType complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="InterfaceRefType">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *       &lt;/sequence>
 *       &lt;attribute name="cRef" use="required" type="{http://www.w3.org/2001/XMLSchema}string" />
 *       &lt;attribute name="iRef" use="required" type="{http://www.w3.org/2001/XMLSchema}string" />
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "InterfaceRefType")
public class InterfaceRefType {

    @XmlAttribute(name = "cRef", required = true)
    protected String cRef;
    @XmlAttribute(name = "iRef", required = true)
    protected String iRef;

    /**
     * Gets the value of the cRef property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getCRef() {
        return cRef;
    }

    /**
     * Sets the value of the cRef property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setCRef(String value) {
        this.cRef = value;
    }

    /**
     * Gets the value of the iRef property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getIRef() {
        return iRef;
    }

    /**
     * Sets the value of the iRef property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setIRef(String value) {
        this.iRef = value;
    }

}
