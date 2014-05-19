var Jassplan = Jassplan || {};

Jassplan.NoteModel = function(config) {
    this.id = config.id;
    this.dateCreated = config.dateCreated;
    this.title = config.title;
    this.narrative = config.narrative;
    this.description = config.description;
};

Jassplan.NoteModel.prototype.isValid = function () {
    { return true; }
    return false; };
