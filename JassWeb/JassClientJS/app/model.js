var Jassplan = Jassplan || {};

Jassplan.NoteModel = function (config) {
    //this is the object model for the entity
    //we still have two ids for historical reason will fix this eventually
    this.id = config.id;
    this.jassActivityID = config.jassActivityID;
    this.dateCreated = config.dateCreated;
    this.title = config.title;
    this.narrative = config.narrative;
    this.description = config.description;
    this.estimatedDuration = config.estimatedDuration;
    if (this.estimatedDuration == null || this.estimatedDuration == "") { this.estimatedDuration = 1; };
    this.estimatedStartHour = config.estimatedStartHour;
    this.doneDate = config.doneDate;
    this.actualDuration = config.actualDuration;
    if (this.actualDuration == null || this.actualDuration == "") { this.actualDuration = 1; };
};

Jassplan.NoteModel.prototype.isValid = function () {
    { return true; }
    return false; };
