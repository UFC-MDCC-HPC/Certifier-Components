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
 * <p>Java class for InterfaceParameterType complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="InterfaceParameterType">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;attribute name="parid" type="{http://www.w3.org/2001/XMLSchema}string" />
 *       &lt;attribute name="varid" type="{http://www.w3.org/2001/XMLSchema}string" />
 *       &lt;attribute name="iname" type="{http://www.w3.org/2001/XMLSchema}string" />
 *       &lt;attribute name="uname" type="{http://www.w3.org/2001/XMLSchema}string" />
 *       &lt;attribute name="order" type="{http://www.w3.org/2001/XMLSchema}int" />
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "InterfaceParameterType")
public class InterfaceParameterType {

    @XmlAttribute(name = "parid")
    protected String parid;
    @XmlAttribute(name = "varid")
    protected String varid;
    @XmlAttribute(name = "iname")
    protected String iname;
    @XmlAttribute(name = "uname")
    protected String uname;
    @XmlAttribute(name = "order")
    protected Integer order;

    /**
     * Gets the value of the parid property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getParid() {
        return parid;
    }

    /**
     * Sets the value of the parid property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setParid(String value) {
        this.parid = value;
    }

    /**
     * Gets the value of the varid property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getVarid() {
        return varid;
    }

    /**
     * Sets the value of the varid property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setVarid(String value) {
        this.varid = value;
    }

    /**
     * Gets the value of the iname property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getIname() {
        return iname;
    }

    /**
     * Sets the value of the iname property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setIname(String value) {
        this.iname = value;
    }

    /**
     * Gets the value of the uname property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getUname() {
        return uname;
    }

    /**
     * Sets the value of the uname property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setUname(String value) {
        this.uname = value;
    }

    /**
     * Gets the value of the order property.
     * 
     * @return
     *     possible object is
     *     {@link Integer }
     *     
     */
    public Integer getOrder() {
        return order;
    }

    /**
     * Sets the value of the order property.
     * 
     * @param value
     *     allowed object is
     *     {@link Integer }
     *     
     */
    public void setOrder(Integer value) {
        this.order = value;
    }

}
