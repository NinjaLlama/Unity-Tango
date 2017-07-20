// --------------------------------------------------------------------------------------------------
// This file was automatically generated by J2CS Translator (http://j2cstranslator.sourceforge.net/). 
// Version 1.3.6.20110331_01     
//
// ${CustomMessageForDisclaimer}                                                                             
// --------------------------------------------------------------------------------------------------
 /// <summary>
/// Copyright (C) 2015-2017 Regents of the University of California.
/// </summary>
///
namespace net.named_data.jndn.encrypt {
	
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.IO;
	using System.Runtime.CompilerServices;
	using net.named_data.jndn.util;
	
	/// <summary>
	/// An EncryptKey supplies the key for encrypt.
	/// </summary>
	///
	/// @note This class is an experimental feature. The API may change.
	public class EncryptKey {
		/// <summary>
		/// Create an EncryptKey with the given key value.
		/// </summary>
		///
		/// <param name="keyBits">The key value.</param>
		public EncryptKey(Blob keyBits) {
			keyBits_ = keyBits;
		}
	
		/// <summary>
		/// Get the key value.
		/// </summary>
		///
		/// <returns>The key value.</returns>
		public Blob getKeyBits() {
			return keyBits_;
		}
	
		private readonly Blob keyBits_;
	}
}
