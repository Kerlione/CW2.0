using System;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LecturerDB.Views
{
    public partial class MainWindow : Form
    {
        private byte[] lecturerCV;
        private byte[] subjectPlan;
        private byte[] subjectDescription;

        public List<string> LectionTypeList = new List<string>() { "Лекция", "Практика", "Лабораторная","Курсовая" };

        public List<int> SemesterList = new List<int>() {1,2,3,4,5,6,7,8};

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cathedraDataSet.Responsibility' table. You can move, or remove it, as needed.
            this.responsibilityTableAdapter.Fill(this.cathedraDataSet.Responsibility);
            // TODO: This line of code loads data into the 'cathedraDataSet.LecturerPublication' table. You can move, or remove it, as needed.
            this.lecturerPublicationTableAdapter.Fill(this.cathedraDataSet.LecturerPublication);
            // TODO: This line of code loads data into the 'cathedraDataSet.LearningResult' table. You can move, or remove it, as needed.
            this.learningResultTableAdapter.Fill(this.cathedraDataSet.LearningResult);
            // TODO: This line of code loads data into the 'cathedraDataSet.Publication' table. You can move, or remove it, as needed.
            this.publicationTableAdapter.Fill(this.cathedraDataSet.Publication);
            // TODO: This line of code loads data into the 'cathedraDataSet.LecturerProject' table. You can move, or remove it, as needed.
            this.lecturerProjectTableAdapter.Fill(this.cathedraDataSet.LecturerProject);
            // TODO: This line of code loads data into the 'cathedraDataSet.Project' table. You can move, or remove it, as needed.
            this.projectTableAdapter.Fill(this.cathedraDataSet.Project);
            // TODO: This line of code loads data into the 'cathedraDataSet.DiplomaDefence' table. You can move, or remove it, as needed.
            this.diplomaDefenceTableAdapter.Fill(this.cathedraDataSet.DiplomaDefence);
            // TODO: This line of code loads data into the 'cathedraDataSet.MoveStudent' table. You can move, or remove it, as needed.
            this.moveStudentTableAdapter.Fill(this.cathedraDataSet.MoveStudent);
            // TODO: This line of code loads data into the 'cathedraDataSet.PlanAccomplishment' table. You can move, or remove it, as needed.
            this.planAccomplishmentTableAdapter.Fill(this.cathedraDataSet.PlanAccomplishment);
            // TODO: This line of code loads data into the 'cathedraDataSet.SubjectReading' table. You can move, or remove it, as needed.
            this.subjectReadingTableAdapter.Fill(this.cathedraDataSet.SubjectReading);
            // TODO: This line of code loads data into the 'cathedraDataSet.Language' table. You can move, or remove it, as needed.
            this.languageTableAdapter.Fill(this.cathedraDataSet.Language);
            // TODO: This line of code loads data into the 'cathedraDataSet.Workload' table. You can move, or remove it, as needed.
            this.workloadTableAdapter.Fill(this.cathedraDataSet.Workload);
            // TODO: This line of code loads data into the 'cathedraDataSet.Subject' table. You can move, or remove it, as needed.
            this.subjectTableAdapter.Fill(this.cathedraDataSet.Subject);
            // TODO: This line of code loads data into the 'cathedra.Group' table. You can move, or remove it, as needed.
            this.groupTableAdapter.Fill(this.cathedraDataSet.Group);
            // TODO: This line of code loads data into the 'cathedra.Lecturer' table. You can move, or remove it, as needed.
            this.lecturerTableAdapter.Fill(this.cathedraDataSet.Lecturer);

        }
        //Lecturer Begin
        //Invoke tab
        private void lecturerTab_Enter(object sender, EventArgs e)
        {
            //lecturerGrid.CancelEdit();
            //lecturerGrid.ClearSelection(); //clear selection of data grid
            //clearLecturerFields();
        }
        //Clear fields
        private void clearLecturerFields()
        {
            try
            {
                lecturerPKMaskedBox.Text = string.Empty;
                lecturerFirstNameTextBox.Text = string.Empty;
                lecturerLastNameTextBox.Text = string.Empty;
                lecturerMiddleNameTextBox.Text = string.Empty;
                lecturerBirthdayDateTimePicker.Value = DateTime.Today;
                lecturerCathedraIDTextBox.Text = string.Empty;
                lecturerOccupationTextBox.Text = string.Empty;
                lecturerDegreeTextBox.Text = string.Empty;
                lecturerLanguagesTextBox.Text = string.Empty;
                lecturerPhotoPictureBox.Image = null;
                lecturerCVLinkLabel.Text = "CV";
                lecturerCV = new byte[0];
            }
            catch (Exception)
            {

            }
        }
        //Enter grid row
        private void lecturerGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            lecturerBindingSource.ResumeBinding();
        }
        //Add photo button
        private void photoAddButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Фотограффия";
                dlg.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    lecturerPhotoPictureBox.Image = new Bitmap(dlg.FileName);
                }
            }
        }
        //Add CV button
        private void lecturerCVAddButton_Click(object sender, EventArgs e) // add cv to a special folder  called CV
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "CV";
                dlg.Filter = "PDF Files|*.pdf";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string curDir = Directory.GetCurrentDirectory();
                    lecturerCV = File.ReadAllBytes(dlg.FileName);
                    Directory.CreateDirectory(curDir+ @"\CV\"); // If the directory already exists, this method does nothing.
                    File.WriteAllBytes(Directory.GetCurrentDirectory()+@"\CV\" + lecturerFirstNameTextBox.Text + lecturerLastNameTextBox.Text + ".pdf", lecturerCV);
                    lecturerCVLinkLabel.Text = @"\CV\"+lecturerFirstNameTextBox.Text + lecturerLastNameTextBox.Text+".pdf";
                }
            }
        }
        //Clear fields button on navigator
        private void lecturerClearFieldsButton_Click(object sender, EventArgs e)
        {
            clearLecturerFields();
        }
        //Add new record button on navigator
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            lecturerBindingSource.ResumeBinding();
        }
        //Save button on navigator
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            if (lecturerPKMaskedBox.Text!="      -")
            {
                try
                {
                    errorProvider1.Clear();
                    TabControlValidation(lecturerTab);
                    lecturerBindingSource.EndEdit();
                    lecturerTableAdapter.Update(cathedraDataSet.Lecturer);
                }
                catch (Exception exc) { MessageBox.Show(exc.Message); }
            }else
            {
                MessageBox.Show("Персональный код не должен быть пустым.");
                errorProvider1.SetError(lecturerPKMaskedBox, "Персональный код не должен быть пустым.");
            }
            
        }
        //Validation?
        private void TabControlValidation(TabPage validatingTab)
        {
            var tabControlList = new HashSet<Control>();
            foreach (Control control in validatingTab.Controls)
            {
                var capturedControl = control; //this is necessary
                control.Validating += (sender, e) =>
                    tabControlList.Add(capturedControl);
                control.Validated += (sender, e) =>
                    tabControlList.Remove(capturedControl);
            }

        }

        private void lecturerViewCVButton_Click(object sender, EventArgs e)
        {
            string filePath = lecturerLastNameTextBox.Text + lecturerFirstNameTextBox.Text + "CV.pdf";
            File.WriteAllBytes(filePath, lecturerCV);
            var pdfViewProcess = Process.Start(filePath);
            if (pdfViewProcess != null)
            {
                pdfViewProcess.EnableRaisingEvents = true;
                pdfViewProcess.Exited += PdfViewProcess_Exited;
            }
        }

        private void PdfViewProcess_Exited(object sender, EventArgs e)
        {
            File.Delete(lecturerLastNameTextBox.Text + lecturerFirstNameTextBox.Text + "CV.pdf");
        }

        private void lecturerPhotoPictureBox_Click(object sender, EventArgs e) //viewing bigger image if required
        {
            if (lecturerPhotoPictureBox.Image != null)
            {

            }
        }

        private void lecturerPKMaskedBox_Validated(object sender, EventArgs e)
        {
            lecturerPublicationListButton.Enabled = lecturerPKMaskedBox.Text != "      -     ";
        }
        //Lecturer End

        //Group Begin
        private void clearGroupFields()
        {
            try
            {
                groupGrid.EndEdit();
                groupNumberTextBox.Text = string.Empty;
                groupStudentCountNumericUpDown.Value = Decimal.Zero;
                groupHeadTextBox.Text = string.Empty;
                groupContactInfoTextBox.Text = string.Empty;
                groupFacultyTextBox.Text = string.Empty;
                groupProgramTextBox.Text = string.Empty;
                groupLearningTypeTextBox.Text = string.Empty;
                groupStartYearNumericUpDown.Value = new Decimal(DateTime.Today.Year);
                groupGrid.BeginEdit(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void groupTab_Enter(object sender, EventArgs e) // needs discussion
        {
            //groupGrid.CancelEdit();
            //groupGrid.ClearSelection(); //clear selection of data grid
            //clearGroupFields();
        }

        private void clearGroupFieldButton_Click(object sender, EventArgs e)
        {
            clearGroupFields();
        }

        private void groupGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (groupGrid.CurrentCell != null)
            {
                groupGrid.BeginEdit(true);
            }
        }

        private void bindingNavigatorAddNewItem1_Click(object sender, EventArgs e)
        {
            groupGrid.CancelEdit();
            //clearGroupFields();
            if (groupGrid.CurrentCell != null)
            {
                groupGrid.BeginEdit(true);
            }
        }

        private void saveToolStripButton1_Click(object sender, EventArgs e)
        {
            if (groupNumberTextBox.Text != string.Empty)
            {
                try
                {
                    errorProvider1.Clear();
                    TabControlValidation(groupTab);
                    groupBindingSource.EndEdit();
                    groupTableAdapter.Update(cathedraDataSet.Group);
                    groupGrid.Refresh();
                }catch(Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }else
            {
                errorProvider1.SetError(groupNumberTextBox, "Номер группы должен быть указан.");
                MessageBox.Show("Номер группы должен быть указан.");
            }
        }

        //Group End

        //Subject Begin

        private void subjectFieldClearButton_Click(object sender, EventArgs e)
        {
            if (subjectGrid.SelectedRows != null)
            {
                subjectGrid.CancelEdit();
                subjectGrid.ClearSelection();
            }
            clearSubjectFields();
        }

        private void clearSubjectFields()
        {
            try
            {
                subjectGrid.CancelEdit();
                subjectCodeTextBox.Text = string.Empty;
                subjectName_rusTextBox.Text = string.Empty;
                subjectName_enTextBox.Text = string.Empty;
                subjectName_lvTextBox.Text = string.Empty;
                subjectKPNumericUpDown.Value = 0;
                subjectCourseWorkCheckBox.Checked = false;
                subjectTestCheckBox.Checked = false;
                subjectLections_DNumericUpDown.Value = 0;
                subjectPractices_DNumericUpDown.Value = 0;
                subjectLaboratories_DNumericUpDown.Value = 0;
                subjectContactHoursDNumericUpDown.Value = 0;
                subjectLections_VNumericUpDown.Value = 0;
                subjectPractices_VNumericUpDown.Value = 0;
                subjectLaboratories_VNumericUpDown.Value = 0;
                subjectLections_ZNumericUpDown.Value = 0;
                subjectPractices_ZNumericUpDown.Value = 0;
                subjectLaboratories_ZNumericUpDown.Value = 0;
                //TODO missing Topic plan & Subject description solution
                //
                subjectGrid.BeginEdit(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void subjectTab_Enter(object sender, EventArgs e) //needs discussion
        {
            //subjectGrid.CancelEdit();
            //subjectGrid.ClearSelection();
            //clearSubjectFields();
        }

        private void saveToolStripButton2_Click(object sender, EventArgs e)
        {
            if (subjectCodeTextBox.Text != string.Empty)
            {
                try
                {
                    errorProvider1.Clear();
                    TabControlValidation(subjectTab);
                    subjectBindingSource.EndEdit();
                    subjectTableAdapter.Update(cathedraDataSet.Subject);
                    subjectGrid.Refresh();
                }
                catch(Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }else
            {
                errorProvider1.SetError(subjectCodeTextBox, "Код предмета должен быть указан");
                MessageBox.Show("Код предмета должен быть указан");
            }
            
        }

        private void bindingNavigatorAddNewItem2_Click(object sender, EventArgs e)
        {
            subjectGrid.CancelEdit();
            clearSubjectFields();
            if (subjectGrid.CurrentCell != null)
            {
                subjectGrid.BeginEdit(true);
            }
        }



        //Subject End

        //Workload start

        private void languageListBox_SelectedValueChanged(object sender, EventArgs e) //TODO fix non updating workload coef
        {
            try
            {
                string lang = workLoadLanguageListBox.GetItemText(workLoadLanguageListBox.SelectedItem);
                workLoadLoadMultiplyNumericUpDown.Value = lang == "English" ? new Decimal(1.5) : new Decimal(1.0);
                workLoadLectionCountNumericUpDown.Value *= workLoadLoadMultiplyNumericUpDown.Value;
                workLoadPracticeCountNumericUpDown.Value *= workLoadLoadMultiplyNumericUpDown.Value;
                workLoadLaboratoryCountNumericUpDown.Value *= workLoadLoadMultiplyNumericUpDown.Value;
                workLoadCourseWorkNumericUpDown.Value *= workLoadLoadMultiplyNumericUpDown.Value;
                workLoadLoadMultiplyNumericUpDown.Validate();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            
        }

        private void newToolStripButton1_Click(object sender, EventArgs e)
        {
            if (workLoadGrid.SelectedRows != null)
            {
                workLoadGrid.CancelEdit();
                workLoadGrid.ClearSelection();
            }
            clearWorkLoadFields();
        }

        private void clearWorkLoadFields()
        {
            try
            {
                workLoadGrid.CancelEdit();
                //workLoadSubjectComboBox.SelectedIndex = 1;
                //workLoadLecturerComboBox.SelectedIndex = 1;
                //workLoadGroupComboBox.SelectedIndex = 1;
                workLoadStudyYearNumericUpDown.Value = DateTime.Today.Year;
                workLoadSemesterListBox.ClearSelected();
                workLoadLectionCountNumericUpDown.Value = 0;
                workLoadPracticeCountNumericUpDown.Value = 0;
                workLoadLaboratoryCountNumericUpDown.Value = 0;
                workLoadCourseWorkNumericUpDown.Value = 0;
                //workLoadLanguageListBox.SelectedIndex = 1;
                workLoadContactHoursNumericUpDown.Value = 0;
                workLoadGrid.BeginEdit(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void workLoadLectionCountNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            workLoadTotalHoursNumericUpDown.Value = workLoadLectionCountNumericUpDown.Value +
                                                    workLoadPracticeCountNumericUpDown.Value +
                                                    workLoadLaboratoryCountNumericUpDown.Value + 
                                                    workLoadCourseWorkNumericUpDown.Value;
        }

        private void saveToolStripButton3_Click(object sender, EventArgs e)
        {
            if (workLoadGroupComboBox.SelectedValue!=null &&
                workLoadLecturerComboBox.SelectedValue!=null &&
                workLoadSubjectComboBox.SelectedValue!=null && workLoadStudyYearNumericUpDown.Value != 0)
            {
                try
                {
                    TabControlValidation(workLoadTab);
                    workloadBindingSource.EndEdit();
                    workloadTableAdapter.Update(cathedraDataSet.Workload);
                    workLoadGrid.Refresh();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }

        private void bindingNavigatorAddNewItem3_Click(object sender, EventArgs e)
        {
            workLoadGrid.CancelEdit();
            clearWorkLoadFields();
            if (workLoadGrid.CurrentCell != null)
            {
                workLoadGrid.BeginEdit(true);
            }
        }



        //Workload end

        //Language begin

        private void bindingNavigatorAddNewItem4_Click(object sender, EventArgs e)
        {
            languageGrid.CancelEdit();
            clearLanguageFields();
            if (languageGrid.CurrentCell != null)
            {
                languageGrid.BeginEdit(true);
            }
        }

        private void newToolStripButton2_Click(object sender, EventArgs e)
        {
            if (languageGrid.SelectedRows != null)
            {
                languageGrid.CancelEdit();
                languageGrid.ClearSelection();
            }
            clearLanguageFields();
        }

        private void clearLanguageFields()
        {
            try
            {
                languageGrid.CancelEdit();
                languageNameTextBox.Text = string.Empty;
                languageGrid.BeginEdit(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void saveToolStripButton4_Click(object sender, EventArgs e)
        {
            if (languageNameTextBox.Text!=string.Empty)
            {
                try
                {
                    TabControlValidation(languageTab);
                    languageBindingSource.EndEdit();
                    languageTableAdapter.Update(cathedraDataSet.Language);
                    languageGrid.Refresh();
                }catch(Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }else
            {
                MessageBox.Show("Поле языка должно быть заполнено.");
                errorProvider1.SetError(languageNameTextBox,"Поле языка должно быть заполнено.");
            }
            
        }



        //Language end

        //LectureReading begin

        private void bindingNavigatorAddNewItem5_Click(object sender, EventArgs e)
        {
            subjectReadingGrid.CancelEdit();
            clearSubjectReading();
            if (subjectReadingGrid.CurrentCell != null)
            {
                subjectReadingGrid.BeginEdit(true);
            }
        }

        private void newToolStripButton3_Click(object sender, EventArgs e)
        {
            if (subjectReadingGrid.SelectedRows != null)
            {
                subjectReadingGrid.CancelEdit();
                subjectReadingGrid.ClearSelection();
            }
            clearSubjectReading();
        }

        private void saveToolStripButton5_Click(object sender, EventArgs e)
        {
            TabControlValidation(languageTab);
            subjectReadingReadYearNumericUpDown.Value = subjectReadingReadYearNumericUpDown.Value;
            subjectReadingBindingSource.EndEdit();
            subjectReadingTableAdapter.Update(cathedraDataSet.SubjectReading);
            subjectReadingGrid.Refresh();
        }

        private void clearSubjectReading()
        {
            try
            {
                subjectReadingGrid.CancelEdit();
                subjectReadingReadYearNumericUpDown.Value = DateTime.Today.Year;
                subjectReadingGrid.BeginEdit(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void languageTab_Enter(object sender, EventArgs e)
        {
            try
            {
                var languageSource = languageTableAdapter.GetData();
                subjectReadingLanguageComboBox.DataSource = languageSource;
                subjectReadingLanguageComboBox.DisplayMember = "LanguageName";
                subjectReadingLanguageComboBox.ValueMember = "LanguageID";
                var subjectSource = subjectTableAdapter.GetData();
                subjectReadingSubjectComboBox.DataSource = subjectSource;
                subjectReadingSubjectComboBox.DisplayMember = "SubjectName_rus";
                subjectReadingSubjectComboBox.ValueMember = "SubjectCode";
                var lecturerSource = lecturerTableAdapter.GetData();
                subjectReadingLecturerComboBox.DataSource = lecturerSource;
                subjectReadingLecturerComboBox.DisplayMember = "FullName";
                subjectReadingLecturerComboBox.ValueMember = "PK";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            
        }


        //LectureReading end

        //Plan accomplishment begin

        private void bindingNavigatorAddNewItem6_Click(object sender, EventArgs e)
        {
            planAccomplishmentGrid.CancelEdit();
            clearPlanAccomplishment();
            if (planAccomplishmentGrid.CurrentCell != null)
            {
                planAccomplishmentGrid.BeginEdit(true);
            }
        }

        private void saveToolStripButton6_Click(object sender, EventArgs e)
        {
            TabControlValidation(planAccomplishmentTab);
            planAccomplishmentBindingSource.EndEdit();
            planAccomplishmentTableAdapter.Update(cathedraDataSet.PlanAccomplishment);
            planAccomplishmentGrid.Refresh();
        }

        private void newToolStripButton4_Click(object sender, EventArgs e)
        {
            if (planAccomplishmentGrid.SelectedRows != null)
            {
                planAccomplishmentGrid.CancelEdit();
                planAccomplishmentGrid.ClearSelection();
            }
            clearPlanAccomplishment();
        }

        private void clearPlanAccomplishment()
        {
            try
            {
                planAccomplishmentGrid.CancelEdit();
                planAccomplishmentEventDateDateTimePicker.Value = DateTime.Today;
                planAccomplishmentLecturePlannedTopicTextBox.Text = string.Empty;
                planAccomplishmentLectureActualTopicTextBox.Text = string.Empty;
                planAccomplishmentHoursNumericUpDown.Value = 0;
                planAccomplishmentGrid.BeginEdit(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void planAccomplishmentTab_Enter(object sender, EventArgs e)
        {
            planAccomplishmentLectureTypeListBox.DataSource = LectionTypeList;
        }


        //Plan accomplishment end

        //Move student begin

        private void newToolStripButton5_Click(object sender, EventArgs e)
        {
            if (moveStudentGrid.SelectedRows != null)
            {
                moveStudentGrid.CancelEdit();
                moveStudentGrid.ClearSelection();
            }
            clearMoveStudent();
        }

        private void bindingNavigatorAddNewItem7_Click(object sender, EventArgs e)
        {
            moveStudentGrid.CancelEdit();
            clearPlanAccomplishment();
            if (moveStudentGrid.CurrentCell != null)
            {
                moveStudentGrid.BeginEdit(true);
            }
        }

        private void saveToolStripButton7_Click(object sender, EventArgs e)
        {
            if (moveStudentGroupComboBox.SelectedValue!=null &&
                moveStudentStudyYearNumericUpDown.Value!=0 &&
                moveStudentStudySemesterListBox.SelectedIndex != 0)
            {
                TabControlValidation(moveStudentTab);
                moveStudentBindingSource.EndEdit();
                moveStudentTableAdapter.Update(cathedraDataSet.MoveStudent);
                moveStudentGrid.Refresh();
            }else
            {

            }

            
        }

        private void clearMoveStudent()
        {
            try
            {
                moveStudentGrid.CancelEdit();
                moveStudentReasonTextBox.Text = string.Empty;
                moveStudentStudyYearNumericUpDown.Value = DateTime.Today.Year;
                moveStudentStudentCountBeginNumericUpDown.Value = 0;
                moveStudentStudentCountEndNumericUpDown.Value = 0;
                moveStudentRemovedNumericUpDown.Value = 0;
                moveStudentGrid.BeginEdit(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void moveStudentTab_Click(object sender, EventArgs e)
        {
            moveStudentStudySemesterListBox.DataSource = SemesterList;
        }

        private void moveStudentStudentCountBeginNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (moveStudentStudentCountBeginNumericUpDown.Value < moveStudentStudentCountEndNumericUpDown.Value)
            {
                moveStudentRemovedNumericUpDown.Value = 0;
            }
            else
            {
                moveStudentRemovedNumericUpDown.Value = moveStudentStudentCountBeginNumericUpDown.Value - moveStudentStudentCountEndNumericUpDown.Value;
            }
        }

        private void moveStudentStudentCountEndNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (moveStudentStudentCountBeginNumericUpDown.Value < moveStudentStudentCountEndNumericUpDown.Value)
            {
                moveStudentRemovedNumericUpDown.Value = 0;
            }
            else
            {
                moveStudentRemovedNumericUpDown.Value = moveStudentStudentCountBeginNumericUpDown.Value - moveStudentStudentCountEndNumericUpDown.Value;
            }
        }




        //Move student end

        //Diploma defence begin

        private void newToolStripButton6_Click(object sender, EventArgs e)
        {
            if (diplomaDefenceGrid.SelectedRows != null)
            {
                diplomaDefenceGrid.CancelEdit();
                diplomaDefenceGrid.ClearSelection();
            }
            clearDiplomaDefence();
        }

        private void saveToolStripButton8_Click(object sender, EventArgs e)
        {
            TabControlValidation(diplomaDefenceTab);
            diplomaDefenceBindingSource.EndEdit();
            diplomaDefenceTableAdapter.Update(cathedraDataSet.DiplomaDefence);
            diplomaDefenceGrid.Refresh();
        }

        private void bindingNavigatorAddNewItem8_Click(object sender, EventArgs e)
        {
            diplomaDefenceGrid.CancelEdit();
            clearDiplomaDefence();
            if (diplomaDefenceGrid.CurrentCell != null)
            {
                diplomaDefenceGrid.BeginEdit(true);
            }

        }

        private void clearDiplomaDefence()
        {
            try
            {
                diplomaDefenceGrid.CancelEdit();
                diplomaDefenceTopicTextBox.Text = string.Empty;
                diplomaDefenceHeadTextBox.Text = string.Empty;
                diplomaDefenceTypeTextBox.Text = string.Empty;
                diplomaDefenceDateDateTimePicker.Value = DateTime.Today;
                diplomaDefenceMarkNumericUpDown.Value = 0;
                diplomaDefenceGrid.BeginEdit(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //Diploma defence end


        //Project begin

        private void newToolStripButton7_Click(object sender, EventArgs e)
        {
            if (projectGrid.SelectedRows != null)
            {
                projectGrid.CancelEdit();
                projectGrid.ClearSelection();
            }
            clearProject();
        }

        private void bindingNavigatorAddNewItem9_Click(object sender, EventArgs e)
        {
            projectGrid.CancelEdit();
            clearProject();
            if (projectGrid.CurrentCell != null)
            {
                projectGrid.BeginEdit(true);
            }
        }

        private void saveToolStripButton9_Click(object sender, EventArgs e)
        {
            if (projectNumberTextBox.Text!=string.Empty)
            {
                try
                {
                    TabControlValidation(projectTab);
                    projectBindingSource.EndEdit();
                    projectTableAdapter.Update(cathedraDataSet.Project);
                    projectGrid.Refresh();
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }else
            {
                MessageBox.Show("Поле номера проекта должно быть заполнено.");
                errorProvider1.SetError(languageNameTextBox, "Поле номера проекта должно быть заполнено.");
            }
            
        }

        private void clearProject()
        {
            try
            {
                projectGrid.CancelEdit();
                projectNameTextBox.Text = string.Empty;
                projectTypeTextBox.Text = string.Empty;
                projectNumberTextBox.Text = string.Empty;
                projectStartDateDateTimePicker.Value = DateTime.Today;
                projectEndDateDateTimePicker.Value = DateTime.Today;
                diplomaDefenceGrid.BeginEdit(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //Project end


        //Project participation begin

        private void bindingNavigatorAddNewItem10_Click(object sender, EventArgs e)
        {
            lecturerProjectGrid.CancelEdit();
            clearProjectLecturer();
            if (lecturerProjectGrid.CurrentCell != null)
            {
                lecturerProjectGrid.BeginEdit(true);
            }
        }

        private void newToolStripButton8_Click(object sender, EventArgs e)
        {
            if (lecturerProjectGrid.SelectedRows != null)
            {
                lecturerProjectGrid.CancelEdit();
                lecturerProjectGrid.ClearSelection();
            }
            clearProjectLecturer();
        }

        private void saveToolStripButton10_Click(object sender, EventArgs e)
        {
            TabControlValidation(lecturerProject);
            lecturerProjectBindingSource.EndEdit();
            lecturerProjectTableAdapter.Update(cathedraDataSet.LecturerProject);
            lecturerProjectGrid.Refresh();
        }


        private void clearProjectLecturer()
        {
            try
            {
                lecturerProjectGrid.CancelEdit();
                noteTextBox.Text = string.Empty;
                participationStartDateDateTimePicker.Value = DateTime.Today;
                participationEndDateDateTimePicker.Value = DateTime.Today;
                lecturerProjectGrid.BeginEdit(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //Project participation end

        //Publication begin

        private void bindingNavigatorAddNewItem11_Click(object sender, EventArgs e)
        {
            publicationGrid.CancelEdit();
            clearPublication();
            if (publicationGrid.CurrentCell != null)
            {
                publicationGrid.BeginEdit(true);
            }
        }

        private void newToolStripButton9_Click(object sender, EventArgs e)
        {
            if (publicationGrid.SelectedRows != null)
            {
                publicationGrid.CancelEdit();
                publicationGrid.ClearSelection();
            }
            clearPublication();
        }

        private void saveToolStripButton11_Click(object sender, EventArgs e)
        {
            try
            {
                TabControlValidation(publicationTab);
                publicationBindingSource.EndEdit();
                publicationTableAdapter.Update(cathedraDataSet.Publication);
                publicationGrid.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearPublication()
        {
            try
            {
                publicationGrid.CancelEdit();
                publicationNameTextBox.Text = string.Empty;
                publicationPlaceTextBox.Text = string.Empty;
                publicationYearNumericUpDown.Value = DateTime.Today.Year;
                publicationRatesTextBox.Text = string.Empty;
                publicationGrid.BeginEdit(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //Publication end

        //Learning result begin

        private void bindingNavigatorAddNewItem12_Click(object sender, EventArgs e)
        {
            learningResultGrid.CancelEdit();
            clearLearningResult();
            if (learningResultGrid.CurrentCell != null)
            {
                learningResultGrid.BeginEdit(true);
            }
        }

        private void newToolStripButton10_Click(object sender, EventArgs e)
        {
            if (learningResultGrid.SelectedRows != null)
            {
                learningResultGrid.CancelEdit();
                learningResultGrid.ClearSelection();
            }
            clearLearningResult();
        }

        private void saveToolStripButton12_Click(object sender, EventArgs e)
        {
            if (learningResultGroupComboBox.SelectedValue!=null &&
                learningResultSubjectComboBox.SelectedValue!=null &&
                learningResultSemesterListBox.SelectedValue!=null)
            {
                try
                {
                    TabControlValidation(learningResultTab);
                    learningResultBindingSource.EndEdit();
                    learningResultTableAdapter.Update(cathedraDataSet.LearningResult);
                    learningResultGrid.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }else
            {

            }
            
        }

        private void clearLearningResult()
        {
            try
            {
                learningResultGrid.CancelEdit();
                learningResultStudyYearNumericUpDown.Value = DateTime.Today.Year;
                learningResultMeanMarkNumericUpDown.Value = 0;
                learningResultFCountNumericUpDown.Value = 0;
                learningResultGrid.BeginEdit(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void learningResultTab_Enter(object sender, EventArgs e)
        {
            try
            {
                learningResultSemesterListBox.DataSource = SemesterList;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        //Learning result end


        //Responsibility begin

        private void bindingNavigatorAddNewItem13_Click(object sender, EventArgs e)
        {
            responsibilityGrid.CancelEdit();
            clearResponsibility();
            if (responsibilityGrid.CurrentCell != null)
            {
                responsibilityGrid.BeginEdit(true);
            }
        }

        private void newToolStripButton11_Click(object sender, EventArgs e)
        {
            if (responsibilityGrid.SelectedRows != null)
            {
                responsibilityGrid.CancelEdit();
                responsibilityGrid.ClearSelection();
            }
            clearResponsibility();
        }

        private void saveToolStripButton13_Click(object sender, EventArgs e)
        {
            try
            {
                TabControlValidation(lecturerPublicationTab);
                responsibilityBindingSource.EndEdit();
                responsibilityTableAdapter.Update(cathedraDataSet.Responsibility);
                responsibilityGrid.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearResponsibility()
        {
            try
            {
                responsibilityGrid.CancelEdit();
                responsibilityDescriptionTextBox.Text = string.Empty;
                responsibilityStartDateDateTimePicker.Value = DateTime.Today;
                responsibilityEndDateDateTimePicker.Value = DateTime.Today;
                learningResultGrid.BeginEdit(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //Responsibility end

        //Lecturer publication begin

        private void bindingNavigatorAddNewItem14_Click(object sender, EventArgs e)
        {
            lecturerPublicationGrid.CancelEdit();
            if (lecturerPublicationGrid.CurrentCell != null)
            {
                lecturerPublicationGrid.BeginEdit(true);
            }
        }

        private void newToolStripButton12_Click(object sender, EventArgs e)
        {
            if (lecturerPublicationGrid.SelectedRows != null)
            {
                lecturerPublicationGrid.CancelEdit();
                lecturerPublicationGrid.ClearSelection();
            }
        }

        private void saveToolStripButton14_Click(object sender, EventArgs e)
        {
            try
            {
                TabControlValidation(lecturerPublicationTab);
                lecturerPublicationBindingSource.EndEdit();
                lecturerPublicationTableAdapter.Update(cathedraDataSet.LecturerPublication);
                responsibilityGrid.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void moveStudentNavigator_RefreshItems(object sender, EventArgs e)
        {

        }

        private void lecturerPublicationListButton_Click(object sender, EventArgs e)
        {
            if (lecturerPKMaskedBox.Text != string.Empty)
            {
                lecturerPublicationTableAdapter.GetData();
            }
        }

        private void responsibilityStartDateDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                responsibilityStartDateDateTimePicker.Focus();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lecturerPKMaskedBox_Validating(object sender, CancelEventArgs e)
        {
            if(lecturerPKMaskedBox.Text.Contains(" "))
            {
                e.Cancel = true;
                lecturerPKMaskedBox.Select(0, lecturerPKMaskedBox.Text.Length);
                errorProvider1.SetError(lecturerPKMaskedBox, "Персональный код должен быть введен!");

            }else
            {
                errorProvider1.Clear();
            }
        }

        private void groupNumberTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (groupNumberTextBox.Text==string.Empty)
            {
                e.Cancel = true;
                groupNumberTextBox.Select(0, groupNumberTextBox.Text.Length);
                errorProvider1.SetError(groupNumberTextBox, "Номер группы должен быть введен!");

            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void subjectCodeTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (subjectCodeTextBox.Text == string.Empty)
            {
                e.Cancel = true;
                groupNumberTextBox.Select(0, subjectCodeTextBox.Text.Length);
                errorProvider1.SetError(subjectCodeTextBox, "Номер группы должен быть введен!");

            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void languageNameTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (languageNameTextBox.Text == string.Empty)
            {
                e.Cancel = true;
                languageNameTextBox.Select(0, languageNameTextBox.Text.Length);
                errorProvider1.SetError(languageNameTextBox, "Номер группы должен быть введен!");

            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void projectNumberTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (projectNumberTextBox.Text == string.Empty)
            {
                e.Cancel = true;
                projectNumberTextBox.Select(0, projectNumberTextBox.Text.Length);
                errorProvider1.SetError(projectNumberTextBox, "Номер проекта должен быть введен!");

            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void lecturerCVLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string filePath = Directory.GetCurrentDirectory() + lecturerCVLinkLabel.Text;
                //lecturerCV = File.ReadAllBytes(filePath);
                var pdfViewProcess = Process.Start(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void lecturerPublicationTab_Enter(object sender, EventArgs e)
        {
            try
            {
                lecturer_PKComboBox.BindingContext = new BindingContext();
                lecturer_PKComboBox.DataSource = lecturerBindingSource;
                lecturer_PKComboBox.DisplayMember = "FullName";
                lecturer_PKComboBox.ValueMember = "PK";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void subjectKPNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                subjectKPHoursTextBox.Text = (subjectKPNumericUpDown.Value * 40).ToString();
                subjectContactHoursDNumericUpDown.Value = subjectKPNumericUpDown.Value * 16;
                selfStudyDayTextBox.Text = (subjectKPNumericUpDown.Value * 24).ToString();
                subjectContactHours_VNumericUpDown.Value = subjectKPNumericUpDown.Value * 12;
                selfStudyVTextBox.Text = (subjectKPNumericUpDown.Value * 28).ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void workLoadTotalHoursNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            workLoadContactHoursNumericUpDown.Value = workLoadTotalHoursNumericUpDown.Value;
        }

        private void subjectContactHoursDNumericUpDown_Validating(object sender, CancelEventArgs e)
        {
            if (subjectContactHoursDNumericUpDown.Value != subjectLections_DNumericUpDown.Value+subjectPractices_DNumericUpDown.Value+subjectLaboratories_DNumericUpDown.Value)
            {
                e.Cancel = true;
                errorProvider1.SetError(subjectContactHoursDNumericUpDown, "Сумма часов лекций, практик и лабораторных должна равняться контактным часам!");

            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void subjectContactHours_VNumericUpDown_Validating(object sender, CancelEventArgs e)
        {
            if (subjectContactHours_VNumericUpDown.Value != subjectLections_VNumericUpDown.Value + subjectPractices_VNumericUpDown.Value + subjectLaboratories_VNumericUpDown.Value)
            {
                e.Cancel = true;
                errorProvider1.SetError(subjectContactHours_VNumericUpDown, "Сумма часов лекций, практик и лабораторных должна равняться контактным часам!");

            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void topicPlanLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                
                    errorProvider1.Clear();
                    string filePath = Directory.GetCurrentDirectory() + topicPlanLinkLabel.Text;
                    //File.WriteAllBytes(filePath, subjectPlan);
                    var pdfViewProcess = Process.Start(filePath);
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void subjectTematicPlanAddButoon_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Тематический план";
                dlg.Filter = "PDF Files|*.pdf";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string curDir = Directory.GetCurrentDirectory();
                    subjectPlan = File.ReadAllBytes(dlg.FileName);
                    Directory.CreateDirectory(curDir + @"\SubjectPlans\"); // If the directory already exists, this method does nothing.
                    File.WriteAllBytes(Directory.GetCurrentDirectory() + @"\SubjectPlans\" + subjectCodeTextBox.Text +subjectName_enTextBox.Text + ".pdf", subjectPlan);
                    topicPlanLinkLabel.Text = @"\SubjectPlans\" + subjectCodeTextBox.Text + subjectName_enTextBox.Text + ".pdf";
                }
            }
        }

        private void subjectDescriptionAddButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Описание предмета";
                dlg.Filter = "PDF Files|*.pdf";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string curDir = Directory.GetCurrentDirectory();
                    subjectDescription = File.ReadAllBytes(dlg.FileName);
                    Directory.CreateDirectory(curDir + @"\SubjectDescriptions\"); // If the directory already exists, this method does nothing.
                    File.WriteAllBytes(Directory.GetCurrentDirectory() + @"\SubjectDescriptions\" + subjectCodeTextBox.Text + subjectName_enTextBox.Text + ".pdf", subjectDescription);
                    subjectDescriptionLinkLabel.Text = @"\SubjectDescriptions\" + subjectCodeTextBox.Text + subjectName_enTextBox.Text + ".pdf";
                }
            }
        }

        private void subjectDescriptionLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                
                    errorProvider1.Clear();
                    string filePath = Directory.GetCurrentDirectory() + subjectDescriptionLinkLabel.Text;
                    //File.WriteAllBytes(filePath, subjectDescription);
                    var pdfViewProcess = Process.Start(filePath);
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        //Lecturer publication end

        /*TODO Fix list

        Проблемы:
           1 - не нажимать "Очистить" при выделенной строке с важной информацией - информация теряется
           2 - не сохраняются PDF в базе данных и соответственно не извлекаются
           3 - не измененные поля не валидируются заново - значения не извлекаются - я не в курсе пока
           4 - проверить якоря разметки на развернутом экране с 3 вкладки - done
           5 - поправить в обязанностях ошибку о невыбранной клетке - ?
           6 - дробные значения не выводятся - поправить в базе значения на decimal - done
           7 - concurrency violation когда сохраняю дважды
         */
    }
}
