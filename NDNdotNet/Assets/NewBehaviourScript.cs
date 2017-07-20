using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using net.named_data.jndn;
using net.named_data.jndn.encoding;
using net.named_data.jndn.transport;
using net.named_data.jndn.util;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log (System.Environment.Version); //.NET version
		try {
			var face = new Face("aleph.ndn.ucla.edu");

			var counter = new Counter();

			// Try to fetch anything.
			var name1 = new Name("/");
			Debug.Log("Express name " + name1.toUri());
			face.expressInterest(name1, counter, counter);

			// Try to fetch using a known name.
			var name2 = new Name("/ndn/edu/ucla/remap/demo/ndn-js-test/hello.txt/%FDU%8D%9DM");
			Debug.Log("Express name " + name2.toUri());
			face.expressInterest(name2, counter, counter);

			// Expect this to time out.
			var name3 = new Name("/test/timeout");
			Debug.Log("Express name " + name3.toUri());
			face.expressInterest(name3, counter, counter);

			// The main event loop.
			while (counter.callbackCount_ < 3) {
				face.processEvents();
				// We need to sleep for a few milliseconds so we don't use 100% of the CPU.
				System.Threading.Thread.Sleep(5);
			}
		} catch (Exception e) {
			Debug.Log("exception: " + e.Message);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
	class Counter : OnData, OnTimeout {
		public void 
		onData(Interest interest, Data data)
		{
			++callbackCount_;

			Debug.Log("Got data packet with name " + data.getName().toUri());
			var content = data.getContent().buf();
			var contentString = "";
			for (int i = content.position(); i < content.limit(); ++i)
				contentString += (char)content.get(i);
			Debug.Log(contentString);
		}

		public void 
		onTimeout(Interest interest)
		{
			++callbackCount_;
			Debug.Log("Time out for interest " + interest.getName().toUri());
		}

		public int callbackCount_ = 0;
	}

/*namespace TestNdnDotNet
{
	class TestGetAsync {
		static void Main(string[] args)
		{
			try {
				var face = new Face("aleph.ndn.ucla.edu");

				var counter = new Counter();

				// Try to fetch anything.
				var name1 = new Name("/");
				Debug.Log("Express name " + name1.toUri());
				face.expressInterest(name1, counter, counter);

				// Try to fetch using a known name.
				var name2 = new Name("/ndn/edu/ucla/remap/demo/ndn-js-test/hello.txt/%FDU%8D%9DM");
				Debug.Log("Express name " + name2.toUri());
				face.expressInterest(name2, counter, counter);

				// Expect this to time out.
				var name3 = new Name("/test/timeout");
				Debug.Log("Express name " + name3.toUri());
				face.expressInterest(name3, counter, counter);

				// The main event loop.
				while (counter.callbackCount_ < 3) {
					face.processEvents();
					// We need to sleep for a few milliseconds so we don't use 100% of the CPU.
					System.Threading.Thread.Sleep(5);
				}
			} catch (Exception e) {
				Debug.Log("exception: " + e.Message);
			}
		}
	}
}*/