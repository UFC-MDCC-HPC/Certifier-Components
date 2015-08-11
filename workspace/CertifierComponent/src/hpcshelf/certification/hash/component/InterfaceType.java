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
 * <p>Java class for InterfaceType complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="InterfaceType">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="slice" type="{http://www.example.org/HashComponent}InterfaceSliceType" maxOccurs="unbounded" minOccurs="0"/>
 *         &lt;element name="sources" type="{http://www.example.org/HashComponent}SourceType" maxOccurs="unbounded" minOccurs="0"/>
 *         &lt;element name="visualDescription" type="{http://www.example.org/HashComponent}VisualElementAttributes"/>
 *         &lt;element name="port" type="{http://www.example.org/HashComponent}InterfacePortType" maxOccurs="unbounded" minOccurs="0"/>
 *         &lt;element name="externalReferences" type="{http://www.w3.org/2001/XMLSchema}string" maxOccurs="unbounded" minOccurs="0"/>
 *         &lt;element name="parameter" type="{http://www.example.org/HashComponent}InterfaceParameterType" maxOccurs="unbounded" minOccurs="0"/>
 *         &lt;element name="action" type="{http://www.example.org/HashComponent}UnitActionType" maxOccurs="unbounded" minOccurs="0"/>
 *         &lt;element name="condition" type="{http://www.example.org/HashComponent}UnitConditionType" maxOccurs="unbounded" minOccurs="0"/>
 *         &lt;element name="protocol" type="{http://www.example.org/HashComponent}ProtocolChoiceType" minOccurs="0"/>
 *       &lt;/sequence>
 *       &lt;attribute name="iRef" use="required" type="{http://www.w3.org/2001/XMLSchema}string" />
 *       &lt;attribute name="nArgs" type="{http://www.w3.org/2001/XMLSchema}int" />
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "InterfaceType", propOrder = {
    "slice",
    "sources",
    "visualDescription",
    "port",
    "externalReferences",
    "parameter",
    "action",
    "condition",
    "protocol"
})
public class InterfaceType {

    protected List<InterfaceSliceType> slice;
    protected List<SourceType> sources;
    @XmlElement(required = true)
    protected VisualElementAttributes visualDescription;
    protected List<InterfacePortType> port;
    protected List<String> externalReferences;
    protected List<InterfaceParameterType> parameter;
    protected List<UnitActionType> action;
    protected List<UnitConditionType> condition;
    protected ProtocolChoiceType protocol;
    @XmlAttribute(name = "iRef", required = true)
    protected String iRef;
    @XmlAttribute(name = "nArgs")
    protected Integer nArgs;

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
     * {@link InterfaceSliceType }
     * 
     * 
     */
    public List<InterfaceSliceType> getSlice() {
        if (slice == null) {
            slice = new ArrayList<InterfaceSliceType>();
        }
        return this.slice;
    }

    /**
     * Gets the value of the sources property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the sources property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getSources().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link SourceType }
     * 
     * 
     */
    public List<SourceType> getSources() {
        if (sources == null) {
            sources = new ArrayList<SourceType>();
        }
        return this.sources;
    }

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
     * Gets the value of the port property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the port property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getPort().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link InterfacePortType }
     * 
     * 
     */
    public List<InterfacePortType> getPort() {
        if (port == null) {
            port = new ArrayList<InterfacePortType>();
        }
        return this.port;
    }

    /**
     * Gets the value of the externalReferences property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the externalReferences property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getExternalReferences().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link String }
     * 
     * 
     */
    public List<String> getExternalReferences() {
        if (externalReferences == null) {
            externalReferences = new ArrayList<String>();
        }
        return this.externalReferences;
    }

    /**
     * Gets the value of the parameter property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the parameter property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getParameter().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link InterfaceParameterType }
     * 
     * 
     */
    public List<InterfaceParameterType> getParameter() {
        if (parameter == null) {
            parameter = new ArrayList<InterfaceParameterType>();
        }
        return this.parameter;
    }

    /**
     * Gets the value of the action property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the action property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getAction().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link UnitActionType }
     * 
     * 
     */
    public List<UnitActionType> getAction() {
        if (action == null) {
            action = new ArrayList<UnitActionType>();
        }
        return this.action;
    }

    /**
     * Gets the value of the condition property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the condition property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getCondition().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link UnitConditionType }
     * 
     * 
     */
    public List<UnitConditionType> getCondition() {
        if (condition == null) {
            condition = new ArrayList<UnitConditionType>();
        }
        return this.condition;
    }

    /**
     * Gets the value of the protocol property.
     * 
     * @return
     *     possible object is
     *     {@link ProtocolChoiceType }
     *     
     */
    public ProtocolChoiceType getProtocol() {
        return protocol;
    }

    /**
     * Sets the value of the protocol property.
     * 
     * @param value
     *     allowed object is
     *     {@link ProtocolChoiceType }
     *     
     */
    public void setProtocol(ProtocolChoiceType value) {
        this.protocol = value;
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

    /**
     * Gets the value of the nArgs property.
     * 
     * @return
     *     possible object is
     *     {@link Integer }
     *     
     */
    public Integer getNArgs() {
        return nArgs;
    }

    /**
     * Sets the value of the nArgs property.
     * 
     * @param value
     *     allowed object is
     *     {@link Integer }
     *     
     */
    public void setNArgs(Integer value) {
        this.nArgs = value;
    }

}