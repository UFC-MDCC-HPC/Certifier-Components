//
// This file was generated by the JavaTM Architecture for XML Binding(JAXB) Reference Implementation, v2.2.4-2 
// See <a href="http://java.sun.com/xml/jaxb">http://java.sun.com/xml/jaxb</a> 
// Any modifications to this file will be lost upon recompilation of the source schema. 
// Generated on: 2015.08.06 at 12:26:07 AM BRT 
//


package hpcshelf.certification.hash.component;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for BaseTypeType complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="BaseTypeType">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="extensionType" type="{http://www.example.org/HashComponent}ExtensionTypeType"/>
 *         &lt;element name="component" type="{http://www.example.org/HashComponent}ComponentInUseType"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "BaseTypeType", propOrder = {
    "extensionType",
    "component"
})
public class BaseTypeType {

    @XmlElement(required = true)
    protected ExtensionTypeType extensionType;
    @XmlElement(required = true)
    protected ComponentInUseType component;

    /**
     * Gets the value of the extensionType property.
     * 
     * @return
     *     possible object is
     *     {@link ExtensionTypeType }
     *     
     */
    public ExtensionTypeType getExtensionType() {
        return extensionType;
    }

    /**
     * Sets the value of the extensionType property.
     * 
     * @param value
     *     allowed object is
     *     {@link ExtensionTypeType }
     *     
     */
    public void setExtensionType(ExtensionTypeType value) {
        this.extensionType = value;
    }

    /**
     * Gets the value of the component property.
     * 
     * @return
     *     possible object is
     *     {@link ComponentInUseType }
     *     
     */
    public ComponentInUseType getComponent() {
        return component;
    }

    /**
     * Sets the value of the component property.
     * 
     * @param value
     *     allowed object is
     *     {@link ComponentInUseType }
     *     
     */
    public void setComponent(ComponentInUseType value) {
        this.component = value;
    }

}
