/* Modernizr 2.6.2 (Custom Build) | MIT & BSD
 * Build: http://modernizr.com/download/#-opacity-generatedcontent-shiv-cssclasses-teststyles-prefixes-css_pointerevents-load
 */

         function DropDown(el) {
             this.dd = el;
             this.placeholder = this.dd.children('a');
             this.opts = this.dd.find('ul.dropdown > li');
             this.val = '';
             this.index = -1;
             this.initEvents();
         }

         DropDown.prototype = {
             initEvents: function () {
                 var obj = this;

                 obj.dd.on('click', function (event) {
                     $(this).toggleClass('active');
                     return false;
                 });

                 obj.opts.on('click', function () {
                     var opt = $(this);
                     //                     obj.val = opt.html();
                     //                     obj.index = opt.index();
                     //                     obj.placeholder.html(obj.val);
                     window.location = opt.find('a').attr('href');
                 });
             },

             getValue: function () {
                 return this.val;
             },
             getIndex: function () {
                 return this.index;
             },
             setValue: function (value) {
                 var opt = this.opts.find('#' + value);
                 this.val = opt.html();
                 this.index = opt.index();
                 this.placeholder.html(this.val);
             }

         }

         $(function () {

             var dd = new DropDown($('#menu'));

             $(document).click(function () {
                 // all dropdowns
                 $('.dropdown-menu').removeClass('active');
             });
         });