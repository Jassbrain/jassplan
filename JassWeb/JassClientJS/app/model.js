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
    this.estimatedStartHour = config.estimatedStartHour;
    this.doneDate = config.doneDate;
    this.actualDuration = config.actualDuration;
    if (!parseInt(this.actualDuration)>0) { this.actualDuration = 1; };
};

Jassplan.NoteModel.prototype.isValid = function () {
    { return true; }
    return false; };
