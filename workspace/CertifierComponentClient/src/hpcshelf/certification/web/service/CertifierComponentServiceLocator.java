/**
 * CertifierComponentServiceLocator.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.4 Apr 22, 2006 (06:55:48 PDT) WSDL2Java emitter.
 */

package hpcshelf.certification.web.service;

public class CertifierComponentServiceLocator extends org.apache.axis.client.Service implements hpcshelf.certification.web.service.CertifierComponentService {

    public CertifierComponentServiceLocator() {
    }


    public CertifierComponentServiceLocator(org.apache.axis.EngineConfiguration config) {
        super(config);
    }

    public CertifierComponentServiceLocator(java.lang.String wsdlLoc, javax.xml.namespace.QName sName) throws javax.xml.rpc.ServiceException {
        super(wsdlLoc, sName);
    }

    // Use to get a proxy class for CertifierComponent
    private java.lang.String CertifierComponent_address = "http://localhost:8080/CertifierComponent/services/CertifierComponent";

    public java.lang.String getCertifierComponentAddress() {
        return CertifierComponent_address;
    }

    // The WSDD service name defaults to the port name.
    private java.lang.String CertifierComponentWSDDServiceName = "CertifierComponent";

    public java.lang.String getCertifierComponentWSDDServiceName() {
        return CertifierComponentWSDDServiceName;
    }

    public void setCertifierComponentWSDDServiceName(java.lang.String name) {
        CertifierComponentWSDDServiceName = name;
    }

    public hpcshelf.certification.web.service.CertifierComponent getCertifierComponent() throws javax.xml.rpc.ServiceException {
       java.net.URL endpoint;
        try {
            endpoint = new java.net.URL(CertifierComponent_address);
        }
        catch (java.net.MalformedURLException e) {
            throw new javax.xml.rpc.ServiceException(e);
        }
        return getCertifierComponent(endpoint);
    }

    public hpcshelf.certification.web.service.CertifierComponent getCertifierComponent(java.net.URL portAddress) throws javax.xml.rpc.ServiceException {
        try {
            hpcshelf.certification.web.service.CertifierComponentSoapBindingStub _stub = new hpcshelf.certification.web.service.CertifierComponentSoapBindingStub(portAddress, this);
            _stub.setPortName(getCertifierComponentWSDDServiceName());
            return _stub;
        }
        catch (org.apache.axis.AxisFault e) {
            return null;
        }
    }

    public void setCertifierComponentEndpointAddress(java.lang.String address) {
        CertifierComponent_address = address;
    }

    /**
     * For the given interface, get the stub implementation.
     * If this service has no port for the given interface,
     * then ServiceException is thrown.
     */
    public java.rmi.Remote getPort(Class serviceEndpointInterface) throws javax.xml.rpc.ServiceException {
        try {
            if (hpcshelf.certification.web.service.CertifierComponent.class.isAssignableFrom(serviceEndpointInterface)) {
                hpcshelf.certification.web.service.CertifierComponentSoapBindingStub _stub = new hpcshelf.certification.web.service.CertifierComponentSoapBindingStub(new java.net.URL(CertifierComponent_address), this);
                _stub.setPortName(getCertifierComponentWSDDServiceName());
                return _stub;
            }
        }
        catch (java.lang.Throwable t) {
            throw new javax.xml.rpc.ServiceException(t);
        }
        throw new javax.xml.rpc.ServiceException("There is no stub implementation for the interface:  " + (serviceEndpointInterface == null ? "null" : serviceEndpointInterface.getName()));
    }

    /**
     * For the given interface, get the stub implementation.
     * If this service has no port for the given interface,
     * then ServiceException is thrown.
     */
    public java.rmi.Remote getPort(javax.xml.namespace.QName portName, Class serviceEndpointInterface) throws javax.xml.rpc.ServiceException {
        if (portName == null) {
            return getPort(serviceEndpointInterface);
        }
        java.lang.String inputPortName = portName.getLocalPart();
        if ("CertifierComponent".equals(inputPortName)) {
            return getCertifierComponent();
        }
        else  {
            java.rmi.Remote _stub = getPort(serviceEndpointInterface);
            ((org.apache.axis.client.Stub) _stub).setPortName(portName);
            return _stub;
        }
    }

    public javax.xml.namespace.QName getServiceName() {
        return new javax.xml.namespace.QName("http://service.web.certification.hpcshelf", "CertifierComponentService");
    }

    private java.util.HashSet ports = null;

    public java.util.Iterator getPorts() {
        if (ports == null) {
            ports = new java.util.HashSet();
            ports.add(new javax.xml.namespace.QName("http://service.web.certification.hpcshelf", "CertifierComponent"));
        }
        return ports.iterator();
    }

    /**
    * Set the endpoint address for the specified port name.
    */
    public void setEndpointAddress(java.lang.String portName, java.lang.String address) throws javax.xml.rpc.ServiceException {
        
if ("CertifierComponent".equals(portName)) {
            setCertifierComponentEndpointAddress(address);
        }
        else 
{ // Unknown Port Name
            throw new javax.xml.rpc.ServiceException(" Cannot set Endpoint Address for Unknown Port" + portName);
        }
    }

    /**
    * Set the endpoint address for the specified port name.
    */
    public void setEndpointAddress(javax.xml.namespace.QName portName, java.lang.String address) throws javax.xml.rpc.ServiceException {
        setEndpointAddress(portName.getLocalPart(), address);
    }

}
