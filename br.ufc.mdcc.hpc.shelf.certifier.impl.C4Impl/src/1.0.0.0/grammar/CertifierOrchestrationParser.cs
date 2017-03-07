using System;
namespace br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.grammar{
public class CertifierOrchestrationParser {

	private CertifierOrchestration certifierOrchestration;
	
	public void readOrchestrationXML(String fileName){
		this.certifierOrchestration = new CertifierOrchestration(fileName);	
	}

	public CertifierOrchestration getOrchestration() {
		return certifierOrchestration;
	}
	
	
	}}
