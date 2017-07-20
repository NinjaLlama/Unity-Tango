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
namespace net.named_data.jndn {
	
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.IO;
	using System.Runtime.CompilerServices;
	using net.named_data.jndn.util;
	
	/// <summary>
	/// A GenericSignature extends Signature and holds the encoding bytes of the
	/// SignatureInfo so that the application can process experimental signature
	/// types. When decoding a packet, if the type of SignatureInfo is not
	/// recognized, the library creates a GenericSignature.
	/// </summary>
	///
	public class GenericSignature : Signature {
		/// <summary>
		/// Create a new GenericSignature with default values.
		/// </summary>
		///
		public GenericSignature() {
			this.signature_ = new Blob();
			this.signatureInfoEncoding_ = new Blob();
			this.typeCode_ = -1;
			this.changeCount_ = 0;
		}
	
		/// <summary>
		/// Create a new GenericSignature with a copy of the fields in the given
		/// signature object.
		/// </summary>
		///
		/// <param name="signature">The signature object to copy.</param>
		public GenericSignature(GenericSignature signature) {
			this.signature_ = new Blob();
			this.signatureInfoEncoding_ = new Blob();
			this.typeCode_ = -1;
			this.changeCount_ = 0;
			signature_ = signature.signature_;
			signatureInfoEncoding_ = signature.signatureInfoEncoding_;
			typeCode_ = signature.typeCode_;
		}
	
		/// <summary>
		/// Return a new GenericSignature which is a deep copy of this
		/// GenericSignature.
		/// </summary>
		///
		/// <returns>A new GenericSignature.</returns>
		/// <exception cref="System.Exception"></exception>
		public override Object Clone() {
			return new GenericSignature(this);
		}
	
		/// <summary>
		/// Get the bytes of the entire signature info encoding (including the type
		/// code).
		/// </summary>
		///
		/// <returns>The encoding bytes. If not specified, the value isNull().</returns>
		public Blob getSignatureInfoEncoding() {
			return signatureInfoEncoding_;
		}
	
		/// <summary>
		/// Set the bytes of the entire signature info encoding (including the type
		/// code).
		/// </summary>
		///
		/// <param name="signatureInfoEncoding">A Blob with the encoding bytes.</param>
		/// <param name="typeCode"></param>
		public void setSignatureInfoEncoding(Blob signatureInfoEncoding,
				int typeCode) {
			signatureInfoEncoding_ = ((signatureInfoEncoding == null) ? new Blob()
					: signatureInfoEncoding);
			typeCode_ = typeCode;
	
			++changeCount_;
		}
	
		/// <summary>
		/// Set the bytes of the entire signature info encoding (including the type
		/// code). getTypeCode() will return -1 for not known.
		/// </summary>
		///
		/// <param name="signatureInfoEncoding">A Blob with the encoding bytes.</param>
		public void setSignatureInfoEncoding(Blob signatureInfoEncoding) {
			setSignatureInfoEncoding(signatureInfoEncoding, -1);
		}
	
		/// <summary>
		/// Get the signature bytes.
		/// </summary>
		///
		/// <returns>The signature bytes. If not specified, the value isNull().</returns>
		public sealed override Blob getSignature() {
			return signature_;
		}
	
		/// <summary>
		/// Set the signature bytes to the given value.
		/// </summary>
		///
		/// <param name="signature">A Blob with the signature bytes.</param>
		public sealed override void setSignature(Blob signature) {
			signature_ = ((signature == null) ? new Blob() : signature);
			++changeCount_;
		}
	
		/// <summary>
		/// Get the type code of the signature type. When wire decode calls
		/// setSignatureInfoEncoding, it sets the type code. Note that the type code
		/// is ignored during wire encode, which simply uses getSignatureInfoEncoding()
		/// where the encoding already has the type code.
		/// </summary>
		///
		/// <returns>The type code, or -1 if not known.</returns>
		public int getTypeCode() {
			return typeCode_;
		}
	
		/// <summary>
		/// Get the change count, which is incremented each time this object
		/// (or a child object) is changed.
		/// </summary>
		///
		/// <returns>The change count.</returns>
		public sealed override long getChangeCount() {
			return changeCount_;
		}
	
		private Blob signature_;
		private Blob signatureInfoEncoding_;
		private int typeCode_;
		private long changeCount_;
	}
}