/**
 * Created by pablo on 12/15/13.
 */

var Jassplan = Jassplan || {};

Jassplan.NoteModel = function(config) {
    this.id = config.id;
    this.dateCreated = config.dateCreated;
    this.title = config.title;
    this.narrative = config.narrative;
};

Jassplan.NoteModel.prototype.isValid = function () {
    "use strict";
    if (this.title && this.title.length > 0)
    { return true; }
    return false; };
