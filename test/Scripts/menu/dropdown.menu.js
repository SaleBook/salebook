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
                     obj.val = opt.html();
                     obj.index = opt.index();
                     obj.placeholder.html(obj.val);
                 });
             },
             getValue: function () {
                 return this.val;
             },
             getIndex: function () {
                 return this.index;
             }
         }

         $(function () {

             var dd = new DropDown($('#dd'));

             $(document).click(function () {
                 // all dropdowns
                 $('.wrapper-dropdown-5').removeClass('active');
             });
         });