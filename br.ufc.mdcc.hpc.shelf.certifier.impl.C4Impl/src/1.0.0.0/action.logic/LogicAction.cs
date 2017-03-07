using System.Threading;

namespace br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.action.logic{

public abstract class LogicAction {
		public string compId;
		public string handleId;
		public Thread _thread;
	public abstract void run();
		public abstract void start ();
}
}