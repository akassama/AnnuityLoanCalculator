// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// JavaScript code.
$(document).ready(function () {


	// Restricts input for each element in the set of matched elements to the given inputFilter.
	(function ($) {
		$.fn.inputFilter = function (inputFilter) {
			return this.on("input keydown keyup mousedown mouseup select contextmenu drop", function () {
				if (inputFilter(this.value)) {
					this.oldValue = this.value;
					this.oldSelectionStart = this.selectionStart;
					this.oldSelectionEnd = this.selectionEnd;
				} else if (this.hasOwnProperty("oldValue")) {
					this.value = this.oldValue;
					this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
				} else {
					this.value = "";
				}
			});
		};
	}(jQuery));

	// Install input filters.
	$(".integer-only").inputFilter(function (value) {
		return /^-?\d*$/.test(value);
	});

	$(".integer-plus-only").inputFilter(function (value) {
		return /^\d*$/.test(value);
	});

	$(".integer-range").inputFilter(function (value) {
		return /^\d*$/.test(value) && (value === "" || parseInt(value) <= 500);
	});

	$(".float-number").inputFilter(function (value) {
		return /^-?\d*[.,]?\d*$/.test(value);
	});

	$(".currency-number").inputFilter(function (value) {
		return /^-?\d*[.,]?\d{0,2}$/.test(value);
	});

	$(".latin-only").inputFilter(function (value) {
		return /^[a-z]*$/i.test(value);
	});

	$(".letters-only").inputFilter(function (value) {
		return /^[a-z ]*$/i.test(value);
	});

	$(".hex-text").inputFilter(function (value) {
		return /^[0-9a-f]*$/i.test(value);
	});
});
