// --------------------------------------------------------------------------------------------------
// This file was automatically generated by J2CS Translator (http://j2cstranslator.sourceforge.net/). 
// Version 1.3.6.20110331_01     
//
// ${CustomMessageForDisclaimer}                                                                             
// --------------------------------------------------------------------------------------------------
 /// <summary>
/// Copyright (C) 2016-2017 Regents of the University of California.
/// </summary>
///
namespace net.named_data.jndn {
	
	using ILOG.J2CsMapping.NIO;
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.IO;
	using System.Runtime.CompilerServices;
	using net.named_data.jndn.encoding;
	using net.named_data.jndn.util;
	
	/// <summary>
	/// A ControlResponse holds a status code, status text and other fields for a
	/// ControlResponse which is used, for example, in the response from sending a
	/// register prefix control command to a forwarder. See
	/// <a href="http://redmine.named-data.net/projects/nfd/wiki/ControlCommand">http://redmine.named-data.net/projects/nfd/wiki/ControlCommand</a>
	/// </summary>
	///
	public class ControlResponse {
		/// <summary>
		/// Create a new ControlResponse where all values are unspecified.
		/// </summary>
		///
		public ControlResponse() {
			this.statusCode_ = -1;
			this.statusText_ = "";
			this.bodyAsControlParameters_ = null;
		}
	
		/// <summary>
		/// Create a new ControlResponse as a deep copy of the given ControlResponse.
		/// </summary>
		///
		/// <param name="controlResponse">The ControlResponse to copy.</param>
		public ControlResponse(ControlResponse controlResponse) {
			this.statusCode_ = -1;
			this.statusText_ = "";
			this.bodyAsControlParameters_ = null;
			statusCode_ = controlResponse.statusCode_;
			statusText_ = controlResponse.statusText_;
			bodyAsControlParameters_ = (controlResponse.bodyAsControlParameters_ == null) ? null
					: new ControlParameters(
							controlResponse.bodyAsControlParameters_);
		}
	
		/// <summary>
		/// Encode this ControlResponse for a particular wire format.
		/// </summary>
		///
		/// <param name="wireFormat">A WireFormat object used to encode this ControlResponse.</param>
		/// <returns>The encoded buffer.</returns>
		public Blob wireEncode(WireFormat wireFormat) {
			return wireFormat.encodeControlResponse(this);
		}
	
		/// <summary>
		/// Encode this ControlResponse for the default wire format
		/// WireFormat.getDefaultWireFormat().
		/// </summary>
		///
		/// <returns>The encoded buffer.</returns>
		public Blob wireEncode() {
			return wireEncode(net.named_data.jndn.encoding.WireFormat.getDefaultWireFormat());
		}
	
		/// <summary>
		/// Decode the input using a particular wire format and update this ControlResponse.
		/// </summary>
		///
		/// <param name="input"></param>
		/// <param name="wireFormat">A WireFormat object used to decode the input.</param>
		/// <exception cref="EncodingException">For invalid encoding.</exception>
		public void wireDecode(ByteBuffer input, WireFormat wireFormat) {
			wireFormat.decodeControlResponse(this, input, true);
		}
	
		/// <summary>
		/// Decode the input using the default wire format
		/// WireFormat.getDefaultWireFormat() and update this ControlResponse.
		/// </summary>
		///
		/// <param name="input"></param>
		/// <exception cref="EncodingException">For invalid encoding.</exception>
		public void wireDecode(ByteBuffer input) {
			wireDecode(input, net.named_data.jndn.encoding.WireFormat.getDefaultWireFormat());
		}
	
		/// <summary>
		/// Decode the input using a particular wire format and update this ControlResponse.
		/// </summary>
		///
		/// <param name="input">The input blob to decode.</param>
		/// <param name="wireFormat">A WireFormat object used to decode the input.</param>
		/// <exception cref="EncodingException">For invalid encoding.</exception>
		public void wireDecode(Blob input, WireFormat wireFormat) {
			wireFormat.decodeControlResponse(this, input.buf(), false);
		}
	
		/// <summary>
		/// Decode the input using the default wire format
		/// WireFormat.getDefaultWireFormat() and update this ControlResponse.
		/// </summary>
		///
		/// <param name="input">The input blob to decode.</param>
		/// <exception cref="EncodingException">For invalid encoding.</exception>
		public void wireDecode(Blob input) {
			wireDecode(input.buf());
		}
	
		/// <summary>
		/// Get the status code.
		/// </summary>
		///
		/// <returns>The status code. If not specified, return -1.</returns>
		public int getStatusCode() {
			return statusCode_;
		}
	
		/// <summary>
		/// Get the status text.
		/// </summary>
		///
		/// <returns>The status text. If not specified, return "".</returns>
		public String getStatusText() {
			return statusText_;
		}
	
		/// <summary>
		/// Get the control response body as a ControlParameters.
		/// </summary>
		///
		/// <returns>The ControlParameters, or null if the body is not specified or if
		/// it is not a ControlParameters.</returns>
		public ControlParameters getBodyAsControlParameters() {
			return bodyAsControlParameters_;
		}
	
		/// <summary>
		/// Set the status code.
		/// </summary>
		///
		/// <param name="statusCode">The status code. If not specified, set to -1.</param>
		/// <returns>This ControlResponse so that you can chain calls to update values.</returns>
		public ControlResponse setStatusCode(int statusCode) {
			statusCode_ = statusCode;
			return this;
		}
	
		/// <summary>
		/// Set the status text.
		/// </summary>
		///
		/// <param name="statusText">The status text. If not specified, set to "".</param>
		/// <returns>This ControlResponse so that you can chain calls to update values.</returns>
		public ControlResponse setStatusText(String statusText) {
			statusText_ = (statusText == null) ? "" : statusText;
			;
			return this;
		}
	
		/// <summary>
		/// Set the control response body as a ControlParameters.
		/// </summary>
		///
		/// <param name="controlParameters">ControlParameters, set to null.</param>
		/// <returns>This ControlResponse so that you can chain calls to update values.</returns>
		public ControlResponse setBodyAsControlParameters(
				ControlParameters controlParameters) {
			bodyAsControlParameters_ = (controlParameters == null) ? null
					: new ControlParameters(controlParameters);
			return this;
		}
	
		private int statusCode_;
		private String statusText_;
		private ControlParameters bodyAsControlParameters_;
	}
}
