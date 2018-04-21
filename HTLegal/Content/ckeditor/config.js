/**
 * @license Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	 config.language = 'vi';
	// config.uiColor = '#AADC6E';
	config.extraPlugins = 'mediaembed,tableresize,imagemaps';
	config.toolbar_Standard = [
		['Source'],['Bold','Italic','Underline','Strike','Subscript','Superscript','-','RemoveFormat'],
		['NumberedList','BulletedList', '-', 'Outdent','Indent', '-', 'Blockquote','CreateDiv', '-','JustifyLeft','JustifyCenter','JustifyRight','JustifyBlock'],['PageBreak'],
		['Styles','Format','Font','FontSize'],['TextColor','BGColor'],['Image','ImageMaps'],['Link','Unlink','Anchor'],['Maximize','ShowBlocks']
	];
	config.toolbar_Basic = [
		['Source'],['Bold', 'Italic', 'Underline', 'Strike','Subscript', 'Superscript','-','RemoveFormat'],
		['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
		['Link', 'Unlink'],['Styles','Format','Font','FontSize'],['TextColor','BGColor'],['Image','ImageMaps'],['PageBreak']
	];
	config.toolbar_Basic2 = [
		['Source'],['Bold', 'Italic', 'Underline', 'Strike','Subscript', 'Superscript','-','RemoveFormat'],
		['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
		['Link', 'Unlink'],
		['Styles','Format','Font','FontSize'],
		['TextColor','BGColor'],
		['Image']		
	];
	config.toolbar_Media = [
		[ 'Source'], ['Image','ImageMaps'], ['Flash','MediaEmbed']
	];
	config.toolbar = [
		[ 'Source', '-', 'NewPage', 'Preview', 'Print', '-', 'Templates' ],
		[ 'Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo' ],
		[ 'Find', 'Replace', '-', 'SelectAll'],
		['Form','Checkbox','Radio','TextField','Textarea','Select','Button','ImageButton','HiddenField'],
		['Image','ImageMaps','Flash','MediaEmbed','Table','HorizontalRule','Smiley','SpecialChar','Iframe'],
		['Link','Unlink','Anchor'],
		'/',
		['Bold','Italic','Underline','Strike','Subscript','Superscript','-','RemoveFormat'],
		['NumberedList','BulletedList','-','Outdent','Indent','-','Blockquote','CreateDiv', '-','JustifyLeft','JustifyCenter','JustifyRight','JustifyBlock'],		
		['Styles','Format','Font','FontSize'],
		['TextColor','BGColor'],
		['Maximize','ShowBlocks','PageBreak']
	];

	config.removePlugins = 'elementspath,resize';
	config.enterMode = CKEDITOR.ENTER_BR;
	config.shiftEnterMode = CKEDITOR.ENTER_P;
	config.autoParagraph = false;
	config.allowedContent = true;
	config.height = '300px';
};
