/**
 * TesteServiceLocator.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.4 Apr 22, 2006 (06:55:48 PDT) WSDL2Java emitter.
 */

package hpcshelf.certification.web.service;

public class TesteServiceLocator extends org.apache.axis.client.Service implements hpcshelf.certification.web.service.TesteService {

    public TesteServiceLocator() {
    }


    public TesteServiceLocator(org.apache.axis.EngineConfiguration config) {
        super(config);
    }

    public TesteServiceLocator(java.lang.String wsdlLoc, javax.xml.namespace.QName sName) throws javax.xml.rpc.ServiceException {
        super(wsdlLoc, sName);
    }

    // Use to get a proxy class for Teste
    private java.lang.String Teste_address = "http://localhost:8080/CertifierComponent/services/Teste";

    public java.lang.String getTesteAddress() {
        return Teste_address;
    }

    // The WSDD service name defaults to the port name.
    private java.lang.String TesteWSDDServiceName = "Teste";

    public java.lang.String getTesteWSDDServiceName() {
        return TesteWSDDServiceName;
    }

    public void setTesteWSDDServiceName(java.lang.String name) {
        TesteWSDDServiceName = name;
    }

    public hpcshelf.certification.web.service.Teste getTeste() throws javax.xml.rpc.ServiceException {
       java.net.URL endpoint;
        try {
            endpoint = new java.net.URL(Teste_address);
        }
        catch (java.net.MalformedURLException e) {
            throw new javax.xml.rpc.ServiceException(e);
        }
        return getTeste(endpoint);
    }

    public hpcshelf.certification.web.service.Teste getTeste(java.net.URL portAddress) throws javax.xml.rpc.ServiceException {
        try {
            hpcshelf.certification.web.service.TesteSoapBindingStub _stub = new hpcshelf.certification.web.service.TesteSoapBindingStub(portAddress, this);
            _stub.setPortName(getTesteWSDDServiceName());
            return _stub;
        }
        catch (org.apache.axis.AxisFault e) {
            return null;
        }
    }

    public void setTesteEndpointAddress(java.lang.String address) {
        Teste_address = address;
    }

    /**
     * For the given interface, get the stub implementation.
     * If this service has no port for the given interface,
     * then ServiceException is thrown.
     */
    public java.rmi.Remote getPort(Class serviceEndpointInterface) throws javax.xml.rpc.ServiceException {
        try {
            if (hpcshelf.certification.web.service.Teste.class.isAssignableFrom(serviceEndpointInterface)) {
                hpcshelf.certification.web.service.TesteSoapBindingStub _stub = new hpcshelf.certification.web.service.TesteSoapBindingStub(new java.net.URL(Teste_address), this);
                _stub.setPortName(getTesteWSDDServiceName());
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
        if ("Teste".equals(inputPortName)) {
            return getTeste();
        }
        else  {
            java.rmi.Remote _stub = getPort(serviceEndpointInterface);
            ((org.apache.axis.client.Stub) _stub).setPortName(portName);
            return _stub;
        }
    }

    public javax.xml.namespace.QName getServiceName() {
        return new javax.xml.namespace.QName("http://service.web.certification.hpcshelf", "TesteService");
    }

    private java.util.HashSet ports = null;

    public java.util.Iterator getPorts() {
        if (ports == null) {
            ports = new java.util.HashSet();
            ports.add(new javax.xml.namespace.QName("http://service.web.certification.hpcshelf", "Teste"));
        }
        return ports.iterator();
    }

    /**
    * Set the endpoint address for the specified port name.
    */
    public void setEndpointAddress(java.lang.String portName, java.lang.String address) throws javax.xml.rpc.ServiceException {
        
if ("Teste".equals(portName)) {
            setTesteEndpointAddress(address);
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
