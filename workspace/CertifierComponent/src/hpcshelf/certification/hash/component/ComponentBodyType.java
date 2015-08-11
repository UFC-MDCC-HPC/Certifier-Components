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
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlElements;
import javax.xml.bind.annotation.XmlType;


/**
 * 
 *         		The elements are in the "workflow order" ...
 *         	
 * 
 * <p>Java class for ComponentBodyType complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="ComponentBodyType">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;choice maxOccurs="unbounded" minOccurs="0">
 *         &lt;element name="innerComponent" type="{http://www.example.org/HashComponent}InnerComponentType"/>
 *         &lt;element name="parameter" type="{http://www.example.org/HashComponent}ParameterType"/>
 *         &lt;element name="supplyParameter" type="{http://www.example.org/HashComponent}ParameterSupplyType"/>
 *         &lt;element name="innerRenaming" type="{http://www.example.org/HashComponent}InnerRenamingType"/>
 *         &lt;element name="fusion" type="{http://www.example.org/HashComponent}FusionType"/>
 *         &lt;element name="split" type="{http://www.example.org/HashComponent}SplitType"/>
 *         &lt;element name="recursiveEntry" type="{http://www.example.org/HashComponent}RecursiveEntryType"/>
 *         &lt;element name="interface" type="{http://www.example.org/HashComponent}InterfaceType"/>
 *         &lt;element name="unit" type="{http://www.example.org/HashComponent}UnitType"/>
 *         &lt;element name="enumerator" type="{http://www.example.org/HashComponent}EnumeratorType"/>
 *         &lt;element name="fusionsOfReplicators" type="{http://www.example.org/HashComponent}FusionsOfReplicatorsType"/>
 *       &lt;/choice>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "ComponentBodyType", propOrder = {
    "innerComponentOrParameterOrSupplyParameter"
})
public class ComponentBodyType {

    @XmlElements({
        @XmlElement(name = "innerComponent", type = InnerComponentType.class),
        @XmlElement(name = "parameter", type = ParameterType.class),
        @XmlElement(name = "supplyParameter", type = ParameterSupplyType.class),
        @XmlElement(name = "innerRenaming", type = InnerRenamingType.class),
        @XmlElement(name = "fusion", type = FusionType.class),
        @XmlElement(name = "split", type = SplitType.class),
        @XmlElement(name = "recursiveEntry", type = RecursiveEntryType.class),
        @XmlElement(name = "interface", type = InterfaceType.class),
        @XmlElement(name = "unit", type = UnitType.class),
        @XmlElement(name = "enumerator", type = EnumeratorType.class),
        @XmlElement(name = "fusionsOfReplicators", type = FusionsOfReplicatorsType.class)
    })
    protected List<Object> innerComponentOrParameterOrSupplyParameter;

    /**
     * Gets the value of the innerComponentOrParameterOrSupplyParameter property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the innerComponentOrParameterOrSupplyParameter property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getInnerComponentOrParameterOrSupplyParameter().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link InnerComponentType }
     * {@link ParameterType }
     * {@link ParameterSupplyType }
     * {@link InnerRenamingType }
     * {@link FusionType }
     * {@link SplitType }
     * {@link RecursiveEntryType }
     * {@link InterfaceType }
     * {@link UnitType }
     * {@link EnumeratorType }
     * {@link FusionsOfReplicatorsType }
     * 
     * 
     */
    public List<Object> getInnerComponentOrParameterOrSupplyParameter() {
        if (innerComponentOrParameterOrSupplyParameter == null) {
            innerComponentOrParameterOrSupplyParameter = new ArrayList<Object>();
        }
        return this.innerComponentOrParameterOrSupplyParameter;
    }

}
