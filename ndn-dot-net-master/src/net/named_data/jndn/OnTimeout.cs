// --------------------------------------------------------------------------------------------------
// This file was automatically generated by J2CS Translator (http://j2cstranslator.sourceforge.net/). 
// Version 1.3.6.20110331_01     
//
// ${CustomMessageForDisclaimer}                                                                             
// --------------------------------------------------------------------------------------------------
 /// <summary>
/// Copyright (C) 2013-2017 Regents of the University of California.
/// </summary>
///
namespace net.named_data.jndn {
	
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.IO;
	using System.Runtime.CompilerServices;
	
	/// <summary>
	/// A class implements OnTimeout if it has onTimeout, used to pass a callback to
	/// Face.expressInterest.
	/// </summary>
	///
	public interface OnTimeout {
		/// <summary>
		/// If the interest times out according to the interest lifetime, onTimeout is
		/// called.
		/// </summary>
		///
		/// <param name="interest">The interest given to expressInterest.</param>
		void onTimeout(Interest interest);
	}
}
