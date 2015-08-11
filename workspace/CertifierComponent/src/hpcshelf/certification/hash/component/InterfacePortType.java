//
// This file was generated by the JavaTM Architecture for XML Binding(JAXB) Reference Implementation, v2.2.4-2 
// See <a href="http://java.sun.com/xml/jaxb">http://java.sun.com/xml/jaxb</a> 
// Any modifications to this file will be lost upon recompilation of the source schema. 
// Generated on: 2015.08.06 at 12:26:07 AM BRT 
//


package hpcshelf.certification.hash.component;

import java.util.ArrayList;
import java.util.List;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlAttribute;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for InterfacePortType complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="InterfacePortType">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="visualDescription" type="{http://www.example.org/HashComponent}VisualElementAttributes"/>
 *         &lt;element name="slice" type="{http://www.example.org/HashComponent}PortSliceType" maxOccurs="unbounded"/>
 *       &lt;/sequence>
 *       &lt;attribute name="name" type="{http://www.w3.org/2001/XMLSchema}string" />
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "InterfacePortType", propOrder = {
    "visualDescription",
    "slice"
})
public class InterfacePortType {

    @XmlElement(required = true)
    protected VisualElementAttributes visualDescription;
    @XmlElement(required = true)
    protected List<PortSliceType> slice;
    @XmlAttribute(name = "name")
    protected String name;

    /**
     * Gets the value of the visualDescription property.
     * 
     * @return
     *     possible object is
     *     {@link VisualElementAttributes }
     *     
     */
    public VisualElementAttributes getVisualDescription() {
        return visualDescription;
    }

    /**
     * Sets the value of the visualDescription property.
     * 
     * @param value
     *     allowed object is
     *     {@link VisualElementAttributes }
     *     
     */
    public void setVisualDescription(VisualElementAttributes value) {
        this.visualDescription = value;
    }

    /**
     * Gets the value of the slice property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the slice property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getSlice().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link PortSliceType }
     * 
     * 
     */
    public List<PortSliceType> getSlice() {
        if (slice == null) {
            slice = new ArrayList<PortSliceType>();
        }
        return this.slice;
    }

    /**
     * Gets the value of the name property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getName() {
        return name;
    }

    /**
     * Sets the value of the name property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setName(String value) {
        this.name = value;
    }

}
