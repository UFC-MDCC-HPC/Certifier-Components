using System;
namespace br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.util{

public class CertifierConsoleLogger {

	public static bool ON = true;
	
	public static void write(String msg){
		if(CertifierConsoleLogger.ON) Console.WriteLine("MSG: "+msg);
	}
}
}