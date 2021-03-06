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
namespace net.named_data.jndn.security {
	
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.IO;
	using System.Runtime.CompilerServices;
	using net.named_data.jndn.util;
	
	/// <summary>
	/// A ValidityPeriod is used in a Data packet's SignatureInfo and represents the
	/// begin and end times of a certificate's validity period.
	/// </summary>
	///
	public class ValidityPeriod : ChangeCountable {
		/// <summary>
		/// Create a default ValidityPeriod where the period is not specified.
		/// </summary>
		///
		public ValidityPeriod() {
			this.notBefore_ = System.Double.MaxValue;
			this.notAfter_ = -System.Double.MaxValue;
			this.changeCount_ = 0;
		}
	
		/// <summary>
		/// Create a new ValidityPeriod with a copy of the fields in the given
		/// validityPeriod.
		/// </summary>
		///
		/// <param name="validityPeriod">The ValidityPeriod to copy.</param>
		public ValidityPeriod(ValidityPeriod validityPeriod) {
			this.notBefore_ = System.Double.MaxValue;
			this.notAfter_ = -System.Double.MaxValue;
			this.changeCount_ = 0;
			notBefore_ = validityPeriod.notBefore_;
			notAfter_ = validityPeriod.notAfter_;
		}
	
		/// <summary>
		/// Check if the period has been set.
		/// </summary>
		///
		/// <returns>True if the period has been set, false if the period is not
		/// specified (after calling the default constructor or clear).</returns>
		public bool hasPeriod() {
			return !(notBefore_ == System.Double.MaxValue && notAfter_ == -System.Double.MaxValue);
		}
	
		/// <summary>
		/// Get the beginning of the validity period range.
		/// </summary>
		///
		/// <returns>The time as milliseconds since Jan 1, 1970 UTC.</returns>
		public double getNotBefore() {
			return notBefore_;
		}
	
		/// <summary>
		/// Get the end of the validity period range.
		/// </summary>
		///
		/// <returns>The time as milliseconds since Jan 1, 1970 UTC.</returns>
		public double getNotAfter() {
			return notAfter_;
		}
	
		/// <summary>
		/// Reset to a default ValidityPeriod where the period is not specified.
		/// </summary>
		///
		public void clear() {
			notBefore_ = System.Double.MaxValue;
			notAfter_ = -System.Double.MaxValue;
			++changeCount_;
		}
	
		/// <summary>
		/// Set the validity period.
		/// </summary>
		///
		/// <param name="notBefore">second.</param>
		/// <param name="notAfter">second.</param>
		/// <returns>This ValidityPeriod so that you can chain calls to update values.</returns>
		public ValidityPeriod setPeriod(double notBefore, double notAfter) {
			// Round up to the nearest second.
			notBefore_ = Math.Round(Math.Ceiling(Math.Round(notBefore,MidpointRounding.AwayFromZero) / 1000.0d) * 1000.0d,MidpointRounding.AwayFromZero);
			// Round down to the nearest second.
			notAfter_ = Math.Round(Math.Floor(Math.Round(notAfter,MidpointRounding.AwayFromZero) / 1000.0d) * 1000.0d,MidpointRounding.AwayFromZero);
			++changeCount_;
	
			return this;
		}
	
		/// <summary>
		/// Check if this is the same validity period as other.
		/// </summary>
		///
		/// <param name="other">The other ValidityPeriod to compare with.</param>
		/// <returns>true if the validity periods are equal.</returns>
		public bool equals(ValidityPeriod other) {
			return notBefore_ == other.notBefore_ && notAfter_ == other.notAfter_;
		}
	
		public override bool Equals(Object other) {
			if (!(other  is  ValidityPeriod))
				return false;
	
			return equals((ValidityPeriod) other);
		}
	
		/// <summary>
		/// Check if the time falls within the validity period.
		/// </summary>
		///
		/// <param name="time">The time to check as milliseconds since Jan 1, 1970 UTC.</param>
		/// <returns>True if the beginning of the validity period is less than or equal
		/// to time and time is less than or equal to the end of the validity period.</returns>
		public bool isValid(double time) {
			return notBefore_ <= time && time <= notAfter_;
		}
	
		/// <summary>
		/// Get the change count, which is incremented each time this object is changed.
		/// </summary>
		///
		/// <returns>The change count.</returns>
		public long getChangeCount() {
			return changeCount_;
		}
	
		private double notBefore_; // MillisecondsSince1970
		private double notAfter_; // MillisecondsSince1970
		private long changeCount_;
	}
}
